using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

class VerificarCatalogos
{
    static int checks;
    static Usuario admin;
    static CategoriaBLL categorias = new CategoriaBLL();
    static ProductoBLL productos = new ProductoBLL();
    static object Sql(string sql, params SqlParameter[] args)
    {
        using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestauranteDB"].ConnectionString))
        using (var cmd = new SqlCommand(sql, cn))
        {
            cmd.Parameters.AddRange(args); cn.Open(); return cmd.ExecuteScalar();
        }
    }
    static void Assert(bool ok, string message)
    {
        if (!ok) throw new Exception(message);
        checks++; Console.WriteLine("OK: " + message);
    }
    static void Rechaza(Action action, string message)
    {
        try { action(); }
        catch (ArgumentException) { Assert(true, message); return; }
        catch (UnauthorizedAccessException) { Assert(true, message); return; }
        throw new Exception("Se acepto una operacion invalida: " + message);
    }
    static void Aislar(Action action)
    {
        SesionActual.Iniciar(admin);
        using (var scope = new TransactionScope()) { action(); } // Sin Complete: rollback.
    }
    static Categoria Categoria()
    {
        var c = new Categoria { Nombre = "Prueba_" + Guid.NewGuid().ToString("N"), Activa = true };
        categorias.GuardarCategoria(c);
        return categorias.ObtenerCategorias().Single(x => x.Nombre == c.Nombre);
    }
    static Producto Producto(Categoria c)
    {
        return new Producto { Nombre = "Prueba_" + Guid.NewGuid().ToString("N"), IdCategoria = c.IdCategoria,
            Codigo = Guid.NewGuid().ToString("N").Substring(0, 20), Precio = 150.50m, Disponible = true };
    }
    static Producto CrearProducto(Categoria c)
    {
        var p = Producto(c); productos.GuardarProducto(p);
        return productos.ObtenerProductos().Single(x => x.Codigo == p.Codigo);
    }
    static int Main()
    {
        try
        {
            var snapshots = new Dictionary<string, object>();
            var counts = new Dictionary<string, int>();
            foreach (string table in new[] { "categoria", "producto", "subcategoria", "detallePedido", "usuario", "empleado" })
            {
                snapshots[table] = Sql("SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM dbo." + table);
                counts[table] = Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo." + table));
            }
            admin = new Usuario { IdUsuario = Convert.ToInt32(Sql("SELECT TOP(1) u.idUsuario FROM dbo.usuario u WHERE idRol=1 AND NOT EXISTS (SELECT 1 FROM dbo.empleado e WHERE e.idUsuario=u.idUsuario AND activo=0) ORDER BY idUsuario")),
                NombreUsuario = "PruebaAdmin", IdRol = 1, NombreRol = "Administrador" };
            Assert(admin.IdUsuario > 0, "Existe administrador activo");
            Aislar(delegate
            {
                var c = Categoria();
                Assert(c.Activa && c.Descripcion == "", "Alta categoria activa con descripcion opcional");
                c.Descripcion = "Descripcion editada"; c.Nombre += "_e"; categorias.GuardarCategoria(c);
                Assert(categorias.BuscarCategorias("Descripcion editada").Any(x => x.IdCategoria == c.IdCategoria), "Edita y busca categoria");
                var p = CrearProducto(c);
                Assert(p.Precio == 150.50m && p.IdSubcategoria == null && p.CategoriaNombre == c.Nombre, "Alta producto conserva precio y categoria sin subcategoria");
                p.Descripcion = "Producto editado"; p.Precio = 199.99m; p.Nombre += "_e"; productos.GuardarProducto(p);
                Assert(productos.BuscarProductos("Producto editado").Any(x => x.IdProducto == p.IdProducto && x.Precio == 199.99m), "Edita y busca producto");
                productos.DesactivarProducto(p.IdProducto);
                Assert(!productos.ObtenerProductos().Single(x => x.IdProducto == p.IdProducto).Disponible, "Retirar conserva producto como no disponible");
                categorias.DesactivarCategoria(c.IdCategoria);
                Assert(!categorias.ObtenerCategorias().Single(x => x.IdCategoria == c.IdCategoria).Activa, "Desactiva categoria sin productos disponibles");
                c.Activa = true; categorias.GuardarCategoria(c); productos.GuardarProducto(p);
                Assert(productos.ObtenerProductos().Single(x => x.IdProducto == p.IdProducto).Disponible, "Reactiva categoria y producto");
            });
            Aislar(delegate
            {
                var c = Categoria(); CrearProducto(c);
                Rechaza(() => categorias.DesactivarCategoria(c.IdCategoria), "Impide desactivar categoria con productos disponibles");
            });
            Aislar(delegate
            {
                var c = Categoria(); CrearProducto(c); c.Activa = false;
                Rechaza(() => categorias.GuardarCategoria(c), "Editar tampoco evade la regla de categoria activa");
            });
            Aislar(delegate
            {
                var c = Categoria(); categorias.DesactivarCategoria(c.IdCategoria);
                Rechaza(() => productos.GuardarProducto(Producto(c)), "Impide ofrecer productos de categoria inactiva");
            });
            Aislar(delegate
            {
                var c = Categoria();
                int s = Convert.ToInt32(Sql("INSERT dbo.subcategoria (idCategoria,nombre) VALUES (@id,'Temporal'); SELECT CONVERT(int,SCOPE_IDENTITY());", new SqlParameter("@id", c.IdCategoria)));
                var p = Producto(c); p.IdSubcategoria = s; productos.GuardarProducto(p);
                Assert(productos.ObtenerSubcategorias().Any(x => x.IdSubcategoria == s), "Lista subcategorias");
                Assert(productos.ObtenerProductos().Single(x => x.Codigo == p.Codigo).SubcategoriaNombre == "Temporal", "Conserva y muestra subcategoria valida");
                p = productos.ObtenerProductos().Single(x => x.Codigo == p.Codigo);
                p.IdSubcategoria = null; productos.GuardarProducto(p);
                Assert(productos.ObtenerProductos().Single(x => x.IdProducto == p.IdProducto).IdSubcategoria == null, "Permite quitar subcategoria");
            });
            Aislar(delegate
            {
                var c = Categoria(); var otra = Categoria();
                int s = Convert.ToInt32(Sql("INSERT dbo.subcategoria (idCategoria,nombre) VALUES (@id,'Temporal'); SELECT CONVERT(int,SCOPE_IDENTITY());", new SqlParameter("@id", otra.IdCategoria)));
                var p = Producto(c); p.IdSubcategoria = s;
                Rechaza(() => productos.GuardarProducto(p), "Rechaza subcategoria de otra categoria");
            });
            Aislar(delegate
            {
                var c = Categoria();
                Rechaza(() => categorias.GuardarCategoria(new Categoria { Nombre = c.Nombre }), "Rechaza categoria duplicada");
            });
            foreach (bool repetirCodigo in new[] { true, false })
                Aislar(delegate
                {
                    var c = Categoria(); var p = CrearProducto(c); var otro = Producto(c);
                    if (repetirCodigo) otro.Codigo = p.Codigo; else otro.Nombre = p.Nombre;
                    Rechaza(() => productos.GuardarProducto(otro), repetirCodigo ? "Rechaza codigo duplicado" : "Rechaza nombre de producto duplicado");
                });

            SesionActual.Iniciar(admin);
            Rechaza(() => categorias.GuardarCategoria(new Categoria { Nombre = " " }), "Valida nombre obligatorio");
            Rechaza(() => categorias.GuardarCategoria(new Categoria { Nombre = new string('x', 61) }), "Valida longitud de categoria");
            Rechaza(() => productos.GuardarProducto(null), "Valida datos de producto ausentes");
            foreach (decimal precio in new[] { 0m, -1m, 1.234m, 100000000m })
            {
                var p = Producto(new Categoria { IdCategoria = 1 }); p.Precio = precio;
                Rechaza(() => productos.GuardarProducto(p), "Valida precio positivo, precision y limite");
            }
            SesionActual.Cerrar();
            Rechaza(() => categorias.DesactivarCategoria(1), "Rechaza cambios sin sesion");
            Aislar(delegate
            {
                bool denegado = false;
                try { new CategoriaDAL().Guardar(new Categoria { Nombre = "No autorizado" }, 0); }
                catch (SqlException ex) { denegado = ex.Number == 50020; }
                Assert(denegado, "SQL exige administrador aunque se omita la BLL");
            });
            foreach (string table in snapshots.Keys)
                Assert(counts[table] == Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo." + table)) && Equals(snapshots[table], Sql("SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM dbo." + table)),
                    "Sin registros temporales ni cambios en datos originales: " + table);
            Console.WriteLine("TOTAL: " + checks + " comprobaciones correctas.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("FALLO: " + ex.GetType().Name);
            if (!(ex is SqlException)) Console.Error.WriteLine(ex.Message);
            return 1;
        }
        finally { SesionActual.Cerrar(); }
    }
}

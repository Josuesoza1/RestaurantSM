using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UICliente;

class VerificarCatalogoCliente
{
    static int checks;
    static BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
    static T Field<T>(object form, string name) { return (T)form.GetType().GetField(name, flags).GetValue(form); }
    static object Invoke(object form, string name, params object[] args) { return form.GetType().GetMethod(name, flags).Invoke(form, args); }
    static void Assert(bool condition, string message) { if (!condition) throw new Exception(message); checks++; Console.WriteLine("OK: " + message); }
    static void Rechaza(Action action, string message)
    {
        try { action(); }
        catch (ArgumentException) { Assert(true, message); return; }
        catch (UnauthorizedAccessException) { Assert(true, message); return; }
        throw new Exception("Se acepto una operacion invalida: " + message);
    }
    static object Sql(string sql)
    {
        using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestauranteDB"].ConnectionString))
        using (var cmd = new SqlCommand(sql, cn)) { cn.Open(); return cmd.ExecuteScalar(); }
    }
    static Producto Product(int id, decimal price, int category)
    {
        return new Producto { IdProducto = id, Nombre = "Producto " + id, Precio = price,
            IdCategoria = category, CategoriaNombre = "Categoria " + category, Disponible = true };
    }
    static void Render(Form form, string name)
    {
        using (var image = new Bitmap(form.Width, form.Height))
        { form.DrawToBitmap(image, new Rectangle(Point.Empty, image.Size)); image.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name)); }
    }
    [STAThread]
    static int Main()
    {
        try
        {
            SesionActual.Cerrar();
            Rechaza(() => new ProductoBLL().ObtenerDisponiblesParaPedido(), "Catalogo requiere sesion");
            SesionActual.Iniciar(new Usuario { IdUsuario = 100, NombreUsuario = "Prueba", IdRol = 3 });
            var actual = new ProductoBLL().ObtenerDisponiblesParaPedido();
            int expected = Convert.ToInt32(Sql("SELECT COUNT(*) FROM producto p JOIN categoria c ON c.idCategoria=p.idCategoria WHERE p.disponible=1 AND c.activa=1"));
            Assert(actual.Count == expected && actual.All(p => p.Disponible), "Catalogo real coincide con productos disponibles y categorias activas");
            Assert(actual.Select(p => p.IdProducto).Distinct().Count() == actual.Count && actual.All(p => p.Precio > 0), "Catalogo sin duplicados y precios positivos");

            var cart = new PedidoBorrador();
            var first = Product(1, 125.25m, 1); var second = Product(2, 35.50m, 2);
            cart.Agregar(first, 2);
            Assert(cart.Total == 250.50m && cart.Lineas[0].Subtotal == 250.50m, "Cantidad por precio calcula subtotal exacto");
            cart.Agregar(second, 1);
            Assert(cart.Total == 286m && cart.Lineas.Count == 2, "Suma productos distintos");
            cart.Agregar(first, 1);
            Assert(cart.Lineas.Count == 2 && cart.Lineas.Single(l => l.IdProducto == 1).Cantidad == 3 && cart.Total == 411.25m, "Acumula producto repetido sin duplicar linea");
            Rechaza(() => cart.Agregar(first, 0), "Rechaza cantidad cero");
            Rechaza(() => cart.Agregar(first, -1), "Rechaza cantidad negativa");
            Rechaza(() => cart.Agregar(first, 997), "Rechaza cantidad acumulada mayor a 999");
            Assert(cart.Total == 411.25m, "Cantidad invalida no modifica la seleccion");
            Rechaza(() => cart.Agregar(null, 1), "Rechaza producto ausente");
            var unavailable = Product(3, 5m, 1); unavailable.Disponible = false;
            Rechaza(() => cart.Agregar(unavailable, 1), "Rechaza producto no disponible");
            Rechaza(() => cart.Agregar(Product(3, 0m, 1), 1), "Rechaza precio cero");
            Rechaza(() => cart.Agregar(Product(3, 1.234m, 1), 1), "Rechaza precio con precision no admitida");
            Rechaza(() => cart.Agregar(Product(3, 99999999.99m, 1), 2), "Rechaza total fuera de limite");
            cart.Quitar(1);
            Assert(cart.Total == 35.50m && cart.Lineas.Count == 1, "Quitar recalcula total");
            Rechaza(() => cart.Quitar(999), "Rechaza linea inexistente");
            cart.Vaciar(); Assert(cart.Total == 0 && cart.Lineas.Count == 0, "Vaciar elimina seleccion en memoria");

            Application.EnableVisualStyles();
            LicenseManager.CurrentContext = new DesigntimeLicenseContext();
            using (var form = new FrmPedidoCliente())
            {
                form.ShowInTaskbar = false; form.StartPosition = FormStartPosition.Manual; form.Location = new Point(-30000, -30000);
                form.Show(); Application.DoEvents();
                form.GetType().GetField("perfilValido", flags).SetValue(form, true);
                Invoke(form, "CargarCatalogo", new Productos { first, second });
                var combo = Field<ComboBox>(form, "cmbProducto");
                var categories = Field<ComboBox>(form, "cmbCategoria");
                Assert(combo.Items.Count == 2 && categories.Items.Count == 3, "Carga catalogo y filtro Todas");
                categories.SelectedValue = 2;
                Assert(combo.Items.Count == 1 && ((Producto)combo.SelectedItem).IdProducto == 2, "Filtra productos de la categoria elegida");
                Assert(!Field<ComboBox>(form, "cmbPrecio").Enabled && Field<ComboBox>(form, "cmbPrecio").SelectedItem.ToString() == second.Precio.ToString("N2"), "Precio automatico no editable");
                Field<NumericUpDown>(form, "nudCantidad").Value = 2;
                Field<Button>(form, "btnAceptar").PerformClick();
                var draft = Field<PedidoBorrador>(form, "borrador");
                Assert(draft.Total == 71m && Field<DataGridView>(form, "dgvDetalle").Rows.Count == 1, "Boton agregar refleja cantidad y subtotal");
                Assert(Field<Label>(form, "lblTotal").Text.Contains(71m.ToString("N2")), "Total visible coincide con la BLL");
                Assert(!Field<Button>(form, "btnEnviar").Enabled && !Field<Button>(form, "btnRegistrarPago").Visible, "No ofrece envio ni cobro sin implementar");
                Render(form, "catalogo-cliente.png");
                Field<Button>(form, "btnQuitar").PerformClick();
                Assert(draft.Total == 0 && draft.Lineas.Count == 0, "Boton quitar actualiza seleccion");
                Invoke(form, "CargarCatalogo", new Productos());
                Assert(combo.Items.Count == 0 && !Field<Button>(form, "btnAceptar").Enabled, "Catalogo vacio bloquea agregar sin fallar");
                form.Size = new Size(1100, 850); Render(form, "catalogo-cliente-ampliado.png");
                form.Hide();
            }
            using (var surface = new DesignSurface(typeof(FrmPedidoCliente)))
                Assert(surface.IsLoaded && surface.LoadErrors.Count == 0, "Disenador del pedido cliente carga");
            Console.WriteLine("TOTAL: " + checks + " comprobaciones correctas; solo SELECT en SQL.");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex.GetType().Name + ": " + ex.Message); return 1; }
        finally { SesionActual.Cerrar(); }
    }
}

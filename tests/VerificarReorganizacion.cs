using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UIEmpleado;
using SistemaPedidoRestaurante.UICliente;

class VerificarReorganizacion
{
    static int checks;
    static void Assert(bool ok, string message) { if (!ok) throw new Exception(message); checks++; Console.WriteLine("OK: " + message); }
    static IEnumerable<Control> Controls(Control root)
    {
        foreach (Control control in root.Controls)
        { yield return control; foreach (Control child in Controls(control)) yield return child; }
    }
    static void Position(Form form)
    {
        form.ShowInTaskbar = false; form.StartPosition = FormStartPosition.Manual; form.Location = new Point(-30000, -30000);
    }
    static void Session(int role)
    {
        SesionActual.Iniciar(new Usuario { IdUsuario = 100 + role, NombreUsuario = "Prueba" + role,
            IdRol = role, NombreRol = new[] { "", "Administrador", "Empleado", "Cliente" }[role] });
    }
    [STAThread]
    static int Main()
    {
        try
        {
            Application.EnableVisualStyles();
            Type[] admin = { typeof(FrmClientes), typeof(FrmMesas), typeof(FrmInventario) };
            Type[] operations = { typeof(FrmPedido), typeof(FrmPago), typeof(FrmFacturas) };
            Assert(admin.All(t => t.Namespace.EndsWith("UIAdministracion")), "Gestiones en Administracion");
            Assert(operations.All(t => t.Namespace.EndsWith("UIEmpleado")), "Pedidos, pagos y facturas en Empleado");
            var assembly = typeof(FrmPedido).Assembly;
            Assert(assembly.GetType("SistemaPedidoRestaurante.UICliente.FrmCompra") == null, "Sin clase antigua FrmCompra");

            using (var owner = new Form())
            {
                Position(owner); owner.Show();
                foreach (int role in new[] { 1, 2, 3 })
                {
                    Session(role);
                    foreach (Type type in admin.Concat(operations))
                    {
                        bool allowed = role == 1 || (role == 2 && operations.Contains(type));
                        using (var form = (Form)Activator.CreateInstance(type))
                        using (var timer = new Timer { Interval = 50 })
                        {
                            Position(form);
                            bool opened = false;
                            Exception failure = null;
                            timer.Tick += delegate
                            {
                                timer.Stop();
                                try
                                {
                                    opened = form.Visible;
                                    Assert(allowed, "Rol autorizado para " + type.Name);
                                    Assert(form.Text.Contains("En preparación"), "Pantalla identifica trabajo pendiente");
                                    Assert(Controls(form).OfType<Button>().Where(b => !b.Text.Trim().StartsWith("Volver")).All(b => !b.Enabled),
                                        "Acciones pendientes deshabilitadas");
                                    Assert(Controls(form).OfType<DataGridView>().All(g => g.ReadOnly && !g.AllowUserToAddRows), "Listado pendiente no permite modificar");
                                }
                                catch (Exception ex) { failure = ex; }
                                finally { form.Close(); }
                            };
                            timer.Start(); form.ShowDialog(owner); timer.Stop();
                            if (failure != null) throw failure;
                            Assert(opened == allowed, "Permiso correcto: rol " + role + " / " + type.Name);
                            Assert(owner.Visible && SesionActual.TieneRol(role) && Application.OpenForms.Count == 1,
                                "Volver conserva propietario y sesion");
                        }
                    }
                }
                owner.Hide();
            }
            Session(1);
            using (var menu = new FrmMenuAdministracion())
            {
                var strip = Controls(menu).OfType<MenuStrip>().Single();
                foreach (string name in new[] { "clientes", "mesas", "inventario", "pedidos", "pagos", "facturas" })
                    Assert(strip.Items.Find(name + "ToolStripMenuItem", true).Length == 1, "Ruta administrativa: " + name);
            }
            Session(2);
            using (var menu = new FrmMenuEmpleado())
            {
                var strip = Controls(menu).OfType<MenuStrip>().Single();
                foreach (string name in new[] { "pedidos", "pagos", "facturas" })
                    Assert(strip.Items.Find(name + "ToolStripMenuItem", true).Length == 1, "Ruta de empleado: " + name);
                Position(menu); menu.Show(); Application.DoEvents();
                using (var image = new Bitmap(menu.Width, menu.Height))
                { menu.DrawToBitmap(image, new Rectangle(Point.Empty, image.Size)); image.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "menu-empleado.png")); }
                menu.Close();
            }
            Session(3);
            using (var menu = new FrmMenuCliente())
            {
                var strip = Controls(menu).OfType<MenuStrip>().Single();
                Assert(strip.Items["hacerPedidoToolStripMenuItem"].Enabled &&
                    !strip.Items["reservarMesaToolStripMenuItem"].Enabled && !strip.Items["verComprasToolStripMenuItem"].Enabled,
                    "Cliente abre su pedido preparado y mantiene reservas e historial pendientes");
            }
            SesionActual.Cerrar();
            LicenseManager.CurrentContext = new DesigntimeLicenseContext();
            foreach (Type type in assembly.GetTypes().Where(t => typeof(Form).IsAssignableFrom(t)))
                using (var surface = new DesignSurface(type))
                    Assert(surface.IsLoaded && surface.LoadErrors.Count == 0, "Disenador: " + type.Name);
            Console.WriteLine("TOTAL: " + checks + " comprobaciones correctas, sin conexion SQL.");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
        finally { SesionActual.Cerrar(); }
    }
}

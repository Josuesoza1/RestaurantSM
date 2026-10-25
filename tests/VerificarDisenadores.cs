using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UIEmpleado;
using SistemaPedidoRestaurante.UICliente;

class VerificarDisenadores
{
    private static int pruebas;
    private sealed class ContextoDiseno : LicenseContext
    {
        public override LicenseUsageMode UsageMode { get { return LicenseUsageMode.Designtime; } }
    }
    private static void Ok(bool condicion, string mensaje)
    {
        if (!condicion) throw new Exception(mensaje);
        pruebas++;
        Console.WriteLine("OK: " + mensaje);
    }
    private static T Campo<T>(object objeto, string nombre)
    {
        return (T)objeto.GetType().GetField(nombre, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objeto);
    }
    private static IEnumerable<Control> Controles(Control raiz)
    {
        foreach (Control control in raiz.Controls)
        {
            yield return control;
            foreach (Control hijo in Controles(control)) yield return hijo;
        }
    }
    private static IEnumerable<ToolStripItem> Opciones(ToolStripItemCollection opciones)
    {
        foreach (ToolStripItem opcion in opciones)
        {
            yield return opcion;
            ToolStripMenuItem grupo = opcion as ToolStripMenuItem;
            if (grupo != null)
                foreach (ToolStripItem hijo in Opciones(grupo.DropDownItems)) yield return hijo;
        }
    }
    private static void EventoClick(Component componente, Type tipo, Type propietario, string nombreEvento = "EventClick")
    {
        object clave = tipo.GetField(nombreEvento, BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
        EventHandlerList eventos = (EventHandlerList)typeof(Component).GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(componente, null);
        Delegate evento = eventos[clave];
        Ok(evento != null && evento.GetInvocationList().Length == 1 &&
            evento.Method.DeclaringType == propietario && !evento.Method.Name.Contains("<"),
            propietario.Name + ": Click unico y nombrado de " + Nombre(componente));
    }
    private static string Nombre(Component componente)
    {
        Control control = componente as Control;
        if (control != null) return control.Name;
        ToolStripItem opcion = componente as ToolStripItem;
        return opcion == null ? componente.GetType().Name : opcion.Name;
    }
    private static void Mostrar(Form formulario)
    {
        formulario.ShowInTaskbar = false;
        formulario.StartPosition = FormStartPosition.Manual;
        formulario.Location = new Point(-20000, -20000);
        formulario.Show();
        Application.DoEvents();
    }
    private static void Capturar(Form formulario, string archivo)
    {
        using (Bitmap imagen = new Bitmap(formulario.Width, formulario.Height))
        {
            formulario.DrawToBitmap(imagen, new Rectangle(0, 0, formulario.Width, formulario.Height));
            imagen.Save(archivo);
        }
    }
    [STAThread]
    static int Main(string[] args)
    {
        LicenseContext contextoAnterior = LicenseManager.CurrentContext;
        try
        {
            LicenseManager.CurrentContext = new ContextoDiseno();
            Form[] formularios = {
                new FrmAjusteInventario(), new FrmConfiguracionRestaurante(), new FrmSeleccionCliente(),
                new FrmConfirmarCobro(), new FrmElegirMesa(), new FrmBandejaOperaciones(),
                new FrmMenuAdministracion(), new FrmMenuEmpleado(), new FrmMenuCliente(),
                new FrmInventario(), new FrmFacturas(), new FrmMisCompras()
            };
            foreach (Form formulario in formularios)
            {
                using (formulario)
                {
                    Mostrar(formulario);
                    Ok(formulario.GetType().GetConstructor(Type.EmptyTypes) != null && Controles(formulario).Any(),
                        formulario.Name + ": constructor sin parametros y controles para disenar");
                    foreach (Button boton in Controles(formulario).OfType<Button>())
                        EventoClick(boton, typeof(Control), formulario.GetType());
                    foreach (MenuStrip menu in Controles(formulario).OfType<MenuStrip>())
                        foreach (ToolStripMenuItem opcion in Opciones(menu.Items).OfType<ToolStripMenuItem>().Where(o => !o.HasDropDownItems && o.Enabled))
                            if (opcion.CheckOnClick) EventoClick(opcion, typeof(ToolStripMenuItem), formulario.GetType(), "EventCheckedChanged");
                            else EventoClick(opcion, typeof(ToolStripItem), formulario.GetType());
                    Capturar(formulario, Path.Combine(args[0], formulario.Name + "-diseno.png"));
                    formulario.Close();
                }
            }
            string[] modos = { "Reservas", "Mesas", "Facturas", "Movimientos" };
            foreach (string modo in modos)
            {
                using (FrmBandejaOperaciones bandeja = new FrmBandejaOperaciones(modo))
                {
                    Mostrar(bandeja);
                    bool correcto = modo == "Reservas" ? Campo<Button>(bandeja, "btnAtendida").Visible && !Campo<Button>(bandeja, "btnNuevo").Visible :
                        modo == "Mesas" ? Campo<Button>(bandeja, "btnLimpiarMesa").Visible :
                        modo == "Facturas" ? Campo<Button>(bandeja, "btnFacturaPdf").Visible && Campo<Button>(bandeja, "btnFacturaPdf").Text == "Guardar PDF" :
                        Campo<FlowLayoutPanel>(bandeja, "acciones").Controls.OfType<Button>().Count(b => b.Visible) == 2;
                    Ok(correcto, "La bandeja conserva las acciones de " + modo);
                    if (modo == "Reservas")
                        Ok(Campo<FlowLayoutPanel>(bandeja, "acciones").Controls.OfType<Button>()
                            .Where(b => b.Visible).Select(b => b.Name)
                            .SequenceEqual(new[] { "btnAtendida", "btnAusencia", "btnCancelar", "btnMesas", "btnRecargar", "btnVolver" }),
                            "La bandeja conserva el orden de las acciones de reservas");
                    Capturar(bandeja, Path.Combine(args[0], "Bandeja-" + modo + ".png"));
                    bandeja.Close();
                }
            }
            using (FrmBandejaOperaciones elegir = new FrmBandejaOperaciones("Pedidos", true))
            {
                Mostrar(elegir);
                Ok(Campo<FlowLayoutPanel>(elegir, "acciones").Controls.OfType<Button>().Count(b => b.Visible) == 3,
                    "Seleccionar pedido muestra solamente seleccionar, recargar y volver");
                elegir.Close();
            }
            using (FrmConfirmarCobro confirmar = new FrmConfirmarCobro(345m, "Efectivo"))
            {
                Mostrar(confirmar);
                Campo<NumericUpDown>(confirmar, "monto").Value = 400m;
                Ok(Campo<Label>(confirmar, "cambio").Text.Contains("55"), "El evento del monto sigue calculando el cambio");
                Campo<Button>(confirmar, "confirmar").PerformClick();
                Ok(confirmar.DialogResult == DialogResult.OK, "El boton de confirmar ejecuta su evento");
            }
            LicenseManager.CurrentContext = contextoAnterior;
            Form[] menus = { new FrmMenuAdministracion(), new FrmMenuEmpleado(), new FrmMenuCliente() };
            for (int i = 0; i < menus.Length; i++)
            {
                using (Form formulario = menus[i])
                {
                    SesionActual.Iniciar(new Usuario { IdUsuario = i + 1, IdRol = i + 1, NombreUsuario = "Prueba" });
                    Mostrar(formulario);
                    MenuStrip menu = Campo<MenuStrip>(formulario, "menuStrip1");
                    int cantidad = i == 0 ? 15 : 6;
                    Ok(Opciones(menu.Items).OfType<ToolStripMenuItem>().Count(o => !o.HasDropDownItems) == cantidad,
                        formulario.Name + ": todas las opciones sin duplicados al ejecutar");
                    formulario.WindowState = FormWindowState.Maximized;
                    Application.DoEvents();
                    Ok(menu.Items.OfType<ToolStripMenuItem>().Count() == cantidad &&
                        menu.Items.Cast<ToolStripItem>().Count(o => o.Name == "cerrarSesionToolStripMenuItem") == 1,
                        formulario.Name + ": maximizado muestra todas las opciones y una sola salida de sesion");
                    Capturar(formulario, Path.Combine(args[0], formulario.Name + "-max.png"));
                    formulario.WindowState = FormWindowState.Normal;
                    formulario.ClientSize = new Size(500, 400);
                    Application.DoEvents();
                    if (i < 2)
                    {
                        int anchoNecesario = Opciones(menu.Items).OfType<ToolStripMenuItem>()
                            .Where(o => !o.HasDropDownItems)
                            .Sum(o => TextRenderer.MeasureText(o.Text, menu.Font).Width + 24) + menu.Padding.Horizontal;
                        Ok(menu.ClientSize.Width >= anchoNecesario || menu.Items.OfType<ToolStripMenuItem>().Any(o => o.HasDropDownItems),
                            formulario.Name + ": agrupa cuando falta espacio al restaurar la ventana");
                    }
                    formulario.Close();
                }
            }
            Console.WriteLine("TOTAL: " + pruebas + " comprobaciones de controles y eventos correctas.");
            return 0;
        }
        catch (Exception error) { Console.Error.WriteLine(error); return 1; }
        finally { LicenseManager.CurrentContext = contextoAnterior; SesionActual.Cerrar(); }
    }
}

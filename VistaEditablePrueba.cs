using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

class VistaEditablePrueba
{
    static BindingFlags flags=BindingFlags.Instance|BindingFlags.NonPublic;
    static object Field(Form f,string name) {return f.GetType().GetField(name,flags).GetValue(f);}
    static void Check(bool ok,string label) {if(!ok)throw new Exception(label);Console.WriteLine("OK "+label);}
    static DocumentoVenta Datos()
    {
        return new DocumentoVenta {EsFactura=true,ImpuestoAgregado=true,Numero="F-PRUEBA",Fecha=DateTime.Now,Cliente="Consumidor final",Moneda="C$",Restaurante="RestaurantSM",Mesa="2",Metodo="Efectivo",Estado="Pagada",AtendidoPor="Empleado",RegistradoPor="Administrador",CobradoPor="Empleado",Subtotal=400,Descuento=40,Impuestos=54,Total=414,Recibido=500,Cambio=86,TasaImpuesto=15,
            Detalle=new List<DetalleDocumento> {new DetalleDocumento {Producto="Producto B",Cantidad=1,PrecioUnitario=200,Subtotal=200},new DetalleDocumento {Producto="Producto A",Cantidad=1,PrecioUnitario=200,Subtotal=200}}};
    }
    static Form Crear(bool resumen)
    {
        var a=typeof(DocumentoPdf).Assembly;
        var venta=Datos();
        var exType=a.GetType("SistemaPedidoRestaurante.UIPrincipal.DocumentoVentaExportacion");
        var exportacion=Activator.CreateInstance(exType,flags,null,new object[]{venta},null);
        var doc=typeof(DocumentoPdf).GetMethod("Crear",BindingFlags.NonPublic|BindingFlags.Static).Invoke(null,new[]{(object)venta,exportacion});
        var save=(Action<string>)Delegate.CreateDelegate(typeof(Action<string>),exportacion,exType.GetMethod("GuardarExcel",flags));
        if(!resumen)return (Form)Activator.CreateInstance(a.GetType("SistemaPedidoRestaurante.UIPrincipal.FrmVistaPrevia"),flags,null,new object[]{doc,venta,save},null);
        var tabla=new DataTable();
        tabla.Columns.Add("Moneda");tabla.Columns.Add("Medio de pago");
        foreach(string n in new[]{"Efectivo recibido","Vuelto","Cobrado","Devuelto por anulación","Neto"})tabla.Columns.Add(n,typeof(decimal));
        tabla.Rows.Add("C$","Efectivo",500m,86m,414m,0m,414m);
        return (Form)Activator.CreateInstance(a.GetType("SistemaPedidoRestaurante.UIPrincipal.FrmVistaPreviaResumen"),flags,null,new object[]{doc,"ResumenPrueba",save,"Resumen diario de caja",tabla,"Notas del resumen de prueba"},null);
    }
    static void Diseno(Form f,bool resumen,int width)
    {
        f.ClientSize=new Size(width,750);
        f.PerformLayout();
        Check(f.GetType().BaseType==typeof(Form),"formulario independiente");
        var panel=(Panel)Field(f,"panelDatos");
        Check(panel.Dock==DockStyle.None,"panel se mueve sin Dock");
        foreach(Control c in panel.Controls)
        {
            Check(c.Dock==DockStyle.None,"control libre "+c.Name);
            Check(panel.ClientRectangle.Contains(c.Bounds),"control dentro "+c.Name+" ancho "+width);
        }
        var list=new List<Control>();foreach(Control c in panel.Controls)list.Add(c);
        for(int i=0;i<list.Count;i++)for(int j=i+1;j<list.Count;j++)
            Check(!list[i].Bounds.IntersectsWith(list[j].Bounds),"sin superposicion "+list[i].Name+"/"+list[j].Name);
        foreach(string n in new[]{"lblTitulo","panelDatos","tablaDetalle","acciones"})
        {
            var c=(Control)Field(f,n);Check(f.ClientRectangle.Contains(c.Bounds),"contenedor dentro "+n);
        }
        var grid=(DataGridView)Field(f,"tablaDetalle");
        Check(!grid.AutoGenerateColumns && grid.Columns.Count==(resumen?7:4),"columnas declaradas en Disenador");
        var save=(SaveFileDialog)Field(f,"dialogoPdf");Check(save.DefaultExt=="pdf","dialogo PDF declarado");
        Check(((SaveFileDialog)Field(f,"dialogoExcel")).DefaultExt=="xlsx","dialogo Excel declarado");
        if(!resumen)
        {
            Check(((TextBox)Field(f,"txtTotal")).Text.Contains("414"),"total en su campo");
            Check(((TextBox)Field(f,"txtDescuento")).Text.Contains("40"),"descuento en su campo");
            Check(((TextBox)Field(f,"txtBaseImpuesto")).Text.Contains("360"),"base imponible en su campo");
            f.BindingContext=new BindingContext();grid.BindingContext=f.BindingContext;
            grid.Sort(grid.Columns["colProducto"],ListSortDirection.Ascending);
            Check(grid.Rows.Count==2 && Convert.ToString(grid.Rows[0].Cells["colProducto"].Value)=="Producto A","orden por producto funciona");
        }
    }
    [STAThread] static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        string modo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"modo.txt");
        if (File.Exists(modo)) { args=new[]{File.ReadAllText(modo).Trim()}; File.Delete(modo); }
        if(args.Length>0)
        {
            using(Form f=Crear(args[0]=="resumen"))Application.Run(f);
            return;
        }
        foreach(bool resumen in new[]{false,true})foreach(int width in new[]{1000,1500})using(Form f=Crear(resumen))Diseno(f,resumen,width);
        DocumentoPdf.Guardar(Datos(),Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"VistaEditable.pdf"));
        Console.WriteLine("COMPROBACIONES COMPLETADAS");
    }
}

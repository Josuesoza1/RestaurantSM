from pathlib import Path
import shutil,tempfile,re

root=Path('SistemaPedidoRestaurante')
folder=root/'UIPrincipal/Documentos'
backup=Path(tempfile.mkdtemp(prefix='RestaurantSM-VistaPrevia-'))
for p in folder.glob('FrmVistaPrevia.*'): shutil.copy2(p,backup/p.name)
shutil.copy2(root/'SistemaPedidoRestaurante.UI.csproj',backup/'UI.csproj')

save_methods='''        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (documento == null) return;
            dialogoPdf.FileName = nombreArchivo + ".pdf";
            Guardar(dialogoPdf, "pdf", documento.GeneratePdf);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (guardarExcel == null) return;
            dialogoExcel.FileName = nombreArchivo + ".xlsx";
            Guardar(dialogoExcel, "xlsx", guardarExcel);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tablaDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            tablaDetalle.ClearSelection();
        }

        private void Guardar(SaveFileDialog elegir, string extension, Action<string> generar)
        {
            try
            {
                if (elegir.ShowDialog(this) != DialogResult.OK) return;
                if (!string.Equals(Path.GetExtension(elegir.FileName), "." + extension, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Use la extensión ." + extension + " para este formato.");
                string destino = Path.GetFullPath(elegir.FileName);
                ArchivoExportado.Guardar(destino, generar);
                MessageBox.Show(this, "El archivo se guardó en la siguiente ubicación." +
                    Environment.NewLine + Environment.NewLine + destino, "Exportación completada",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "No se pudo guardar el archivo. " + error.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
'''
code='''using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal partial class FrmVistaPrevia : Form
    {
        private readonly IDocument documento;
        private readonly string nombreArchivo;
        private readonly Action<string> guardarExcel;

        public FrmVistaPrevia()
        {
            InitializeComponent();
        }

        internal FrmVistaPrevia(IDocument documento, DocumentoVenta venta, Action<string> guardarExcel) : this()
        {
            if (venta == null) throw new ArgumentException("No se recibió la venta para mostrar.");
            this.documento = documento;
            nombreArchivo = venta.Numero;
            this.guardarExcel = guardarExcel;
            Text = "Vista previa - " + venta.Numero;
            lblTitulo.Text = (venta.EsFactura ? "Factura " : "Comprobante de venta ") + venta.Numero;
            txtNumero.Text = venta.Numero;
            txtFecha.Text = venta.Fecha.ToString("dd/MM/yyyy HH:mm");
            txtCliente.Text = venta.Cliente;
            txtMetodo.Text = string.IsNullOrWhiteSpace(venta.Metodo) ? "Sin cobrar" : venta.Metodo;
            txtMesa.Text = string.IsNullOrWhiteSpace(venta.Mesa) ? "No aplica" : venta.Mesa;
            txtAtendido.Text = string.IsNullOrWhiteSpace(venta.AtendidoPor) ? "Sin registrar" : venta.AtendidoPor;
            txtEstado.Text = venta.Estado;
            txtRestaurante.Text = venta.Restaurante;
            txtRegistrado.Text = venta.RegistradoPor;
            decimal subtotal = venta.ImpuestoAgregado ? venta.Subtotal : venta.Detalle.Sum(linea => linea.Subtotal);
            txtSubtotal.Text = Dinero(venta, subtotal);
            txtDescuento.Text = Dinero(venta, venta.Descuento);
            txtBaseImpuesto.Text = Dinero(venta, venta.ImpuestoAgregado ? venta.Subtotal - venta.Descuento : venta.Subtotal);
            txtImpuesto.Text = Dinero(venta, venta.Impuestos) +
                (venta.TasaImpuesto >= 0 ? " (" + venta.TasaImpuesto.ToString("0.##") + " %)" : "") +
                (venta.EsFactura && !venta.ImpuestoAgregado ? " incluido" : "");
            txtTotal.Text = Dinero(venta, venta.Total);
            txtRecibido.Text = venta.EsFactura ? Dinero(venta, venta.Recibido) : "Sin cobrar";
            txtVuelto.Text = venta.EsFactura ? Dinero(venta, venta.Cambio) : "Sin cobrar";
            tablaDetalle.DataSource = venta.Detalle;
        }

        private static string Dinero(DocumentoVenta venta, decimal importe)
        {
            return venta.Moneda + " " + importe.ToString("N2");
        }

'''+save_methods+'''    }
}
'''
(folder/'FrmVistaPrevia.cs').write_text(code,encoding='utf-8-sig')

summary='''using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal partial class FrmVistaPreviaResumen : Form
    {
        private readonly IDocument documento;
        private readonly string nombreArchivo;
        private readonly Action<string> guardarExcel;

        public FrmVistaPreviaResumen()
        {
            InitializeComponent();
        }

        internal FrmVistaPreviaResumen(IDocument documento, string nombreArchivo, Action<string> guardarExcel,
            string titulo, DataTable filas, string datos = "") : this()
        {
            this.documento = documento;
            this.nombreArchivo = nombreArchivo;
            this.guardarExcel = guardarExcel;
            Text = "Vista previa - " + nombreArchivo;
            lblTitulo.Text = titulo;
            lblNotas.Text = datos;
            tablaDetalle.DataSource = filas;
        }

'''+save_methods+'''    }
}
'''
(folder/'FrmVistaPreviaResumen.cs').write_text(summary,encoding='utf-8-sig')

for summary_mode,name in [(False,'FrmVistaPrevia'),(True,'FrmVistaPreviaResumen')]:
    fields=[('lblTitulo','Label'),('panelDatos','Panel'),('tablaDetalle','DataGridView'),('acciones','Panel'),('btnPdf','Button'),('btnExcel','Button'),('btnCerrar','Button'),('dialogoPdf','SaveFileDialog'),('dialogoExcel','SaveFileDialog')]
    values=[('Numero','Número'),('Fecha','Fecha'),('Cliente','Cliente'),('Metodo','Forma de pago'),('Mesa','Mesa'),('Atendido','Atendido por'),('Estado','Estado'),('Restaurante','Restaurante'),('Subtotal','Subtotal'),('Descuento','Descuento'),('BaseImpuesto','Base para el impuesto'),('Impuesto','Impuesto'),('Total','Total'),('Recibido','Recibido'),('Vuelto','Vuelto'),('Registrado','Registrado por')]
    if summary_mode: fields.append(('lblNotas','Label'))
    else:
        for suffix,label in values: fields.extend([('lbl'+suffix,'Label'),('txt'+suffix,'TextBox')])
    columns=[('colProducto','Producto','Producto',260),('colCantidad','Cantidad','Cantidad',80),('colPrecio','Precio unitario','PrecioUnitario',130),('colImporte','Importe','Subtotal',130)]
    if summary_mode:
        columns=[('colMoneda','Moneda','Moneda',75),('colMedio','Medio de pago','Medio de pago',160),('colRecibido','Efectivo recibido','Efectivo recibido',130),('colVuelto','Vuelto','Vuelto',100),('colCobrado','Cobrado','Cobrado',110),('colDevuelto','Devuelto por anulación','Devuelto por anulación',160),('colNeto','Neto','Neto',110)]
    fields.extend((n,'DataGridViewTextBoxColumn') for n,_,_,_ in columns)
    declarations='\n'.join('        private System.Windows.Forms.'+typ+' '+n+';' for n,typ in fields)
    lines=['            this.components = new System.ComponentModel.Container();',f'            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof({name}));']
    for n,typ in fields: lines.append(f'            this.{n} = new System.Windows.Forms.{typ}();')
    lines+=['            this.SuspendLayout();','            this.panelDatos.SuspendLayout();','            this.acciones.SuspendLayout();','            ((System.ComponentModel.ISupportInitialize)(this.tablaDetalle)).BeginInit();']
    def prop(n,p,v): lines.append(f'            this.{n}.{p} = {v};')
    def bounds(n,x,y,w,h):
        prop(n,'Location',f'new System.Drawing.Point({x}, {y})');prop(n,'Size',f'new System.Drawing.Size({w}, {h})')
    def anchor(n,value):prop(n,'Anchor',value)
    all_anchor='System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right'
    top_wide='System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right'
    for n,_ in fields:
        if not n.startswith('col'):prop(n,'Name','"'+n+'"')
    prop('lblTitulo','BackColor','System.Drawing.Color.Red');prop('lblTitulo','ForeColor','System.Drawing.Color.White')
    prop('lblTitulo','Text','"Vista previa del resumen"' if summary_mode else '"Vista previa de la factura"')
    prop('lblTitulo','Font','new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold)')
    prop('lblTitulo','Padding','new System.Windows.Forms.Padding(18, 0, 0, 0)');prop('lblTitulo','TextAlign','System.Drawing.ContentAlignment.MiddleLeft')
    bounds('lblTitulo',0,0,1120,64);anchor('lblTitulo',top_wide)
    prop('panelDatos','BackColor','System.Drawing.Color.FromArgb(255, 128, 0)')
    bounds('panelDatos',14,80,1092,140 if summary_mode else 360);anchor('panelDatos',top_wide)
    if summary_mode:
        prop('lblNotas','Text','"El resumen incluye los cobros y las devoluciones del período seleccionado. Los importes aparecen por medio de pago en la tabla inferior."')
        prop('lblNotas','BackColor','System.Drawing.Color.White');prop('lblNotas','Padding','new System.Windows.Forms.Padding(12)')
        bounds('lblNotas',12,12,1068,116);anchor('lblNotas',all_anchor)
        lines.append('            this.panelDatos.Controls.Add(this.lblNotas);')
    else:
        for i,(suffix,label) in enumerate(values):
            side=0 if i<8 else 1;row=i%8
            capx=16 if side==0 else 550;valx=130 if side==0 else 710
            labelname='lbl'+suffix;textbox='txt'+suffix
            prop(labelname,'Text','"'+label+'"');prop(labelname,'TextAlign','System.Drawing.ContentAlignment.MiddleLeft')
            bounds(labelname,capx,14+row*42,110 if side==0 else 150,30)
            bounds(textbox,valx,14+row*42,290 if side==0 else 350,30)
            prop(textbox,'ReadOnly','true');prop(textbox,'BackColor','System.Drawing.Color.White');prop(textbox,'Text','"—"');prop(textbox,'TabStop','false');prop(textbox,'AccessibleName','"'+label+'"')
            if side:
                anchor(labelname,'System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right');anchor(textbox,'System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right')
            if suffix=='Total':prop(textbox,'Font','new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold)')
            lines.append(f'            this.panelDatos.Controls.Add(this.{labelname});');lines.append(f'            this.panelDatos.Controls.Add(this.{textbox});')
    prop('tablaDetalle','AccessibleName','"Detalle del resumen"' if summary_mode else '"Productos de la factura"')
    prop('tablaDetalle','AutoGenerateColumns','false');prop('tablaDetalle','AllowUserToAddRows','false');prop('tablaDetalle','AllowUserToDeleteRows','false');prop('tablaDetalle','ReadOnly','true');prop('tablaDetalle','RowHeadersVisible','false')
    prop('tablaDetalle','BackgroundColor','System.Drawing.Color.White');prop('tablaDetalle','AutoSizeColumnsMode','System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill');prop('tablaDetalle','AutoSizeRowsMode','System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells')
    prop('tablaDetalle','ColumnHeadersHeightSizeMode','System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize')
    prop('tablaDetalle','DefaultCellStyle.WrapMode','System.Windows.Forms.DataGridViewTriState.True')
    bounds('tablaDetalle',14,236 if summary_mode else 456,1092,430 if summary_mode else 210);anchor('tablaDetalle',all_anchor)
    lines.append('            this.tablaDetalle.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.tablaDetalle_DataBindingComplete);')
    for n,title,data,weight in columns:
        prop(n,'Name','"'+n+'"');prop(n,'HeaderText','"'+title+'"');prop(n,'DataPropertyName','"'+data+'"');prop(n,'FillWeight',str(weight)+'F');prop(n,'MinimumWidth','65')
        isnumber=(not summary_mode and n in ['colPrecio','colImporte']) or (summary_mode and n not in ['colMoneda','colMedio'])
        if isnumber:
            prop(n,'DefaultCellStyle.Format','"N2"');prop(n,'DefaultCellStyle.Alignment','System.Windows.Forms.DataGridViewContentAlignment.MiddleRight')
        lines.append(f'            this.tablaDetalle.Columns.Add(this.{n});')
    prop('acciones','BackColor','System.Drawing.Color.FromArgb(255, 255, 128)');bounds('acciones',14,680,1092,56);anchor('acciones','System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right')
    for n,title,x,w,color,handler in [('btnPdf','Guardar PDF',12,154,'255, 128, 128','btnPdf_Click'),('btnExcel','Guardar Excel',182,164,'0, 192, 0','btnExcel_Click'),('btnCerrar','Cerrar',362,120,'255, 192, 192','btnCerrar_Click')]:
        bounds(n,x,10,w,36);prop(n,'Text','"'+title+'"');prop(n,'BackColor','System.Drawing.Color.FromArgb('+color+')');prop(n,'UseVisualStyleBackColor','false')
        lines.append(f'            this.{n}.Click += new System.EventHandler(this.{handler});');lines.append(f'            this.acciones.Controls.Add(this.{n});')
    prop('btnCerrar','DialogResult','System.Windows.Forms.DialogResult.Cancel')
    prop('dialogoPdf','Filter','"Documento PDF (*.pdf)|*.pdf"');prop('dialogoPdf','DefaultExt','"pdf"')
    prop('dialogoExcel','Filter','"Libro de Excel (*.xlsx)|*.xlsx"');prop('dialogoExcel','DefaultExt','"xlsx"')
    lines+=['            this.components.Add(this.dialogoPdf);','            this.components.Add(this.dialogoExcel);']
    for n in ['lblTitulo','panelDatos','tablaDetalle','acciones']:lines.append(f'            this.Controls.Add(this.{n});')
    lines+=['            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);','            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;','            this.Font = new System.Drawing.Font("Segoe UI", 10F);','            this.ClientSize = new System.Drawing.Size(1120, 750);','            this.MinimumSize = new System.Drawing.Size(1016, 719);','            this.CancelButton = this.btnCerrar;','            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));',f'            this.Name = "{name}";','            this.Text = "Vista previa";','            this.ShowInTaskbar = false;','            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;','            ((System.ComponentModel.ISupportInitialize)(this.tablaDetalle)).EndInit();','            this.panelDatos.ResumeLayout(false);','            this.panelDatos.PerformLayout();','            this.acciones.ResumeLayout(false);','            this.ResumeLayout(false);']
    designer='''namespace SistemaPedidoRestaurante.UIPrincipal
{
    partial class '''+name+'''
    {
        private System.ComponentModel.IContainer components = null;
'''+declarations+'''

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
'''+ '\n'.join(lines)+'''
        }
    }
}
'''
    (folder/(name+'.Designer.cs')).write_text(designer,encoding='utf-8-sig')
shutil.copy2(folder/'FrmVistaPrevia.resx',folder/'FrmVistaPreviaResumen.resx')
p=folder/'DocumentoPdf.cs';s=p.read_text(encoding='utf-8-sig');s=s.replace('new FrmVistaPrevia(pdf, documento.Numero, exportacion.GuardarExcel,\n                exportacion.Titulo, exportacion.Detalle, exportacion.DatosVista(), exportacion.ImportesVista())','new FrmVistaPrevia(pdf, documento, exportacion.GuardarExcel)');p.write_text(s,encoding='utf-8-sig')
for p in root.rglob('*.cs'):
    if 'bin' in p.parts or 'obj' in p.parts:continue
    s=p.read_text(encoding='utf-8-sig')
    if 'new FrmVistaPrevia(reporte.' in s:
        p.write_text(s.replace('new FrmVistaPrevia(reporte.','new FrmVistaPreviaResumen(reporte.'),encoding='utf-8-sig')
p=root/'SistemaPedidoRestaurante.UI.csproj';s=p.read_text(encoding='utf-8-sig')
entries='''    <Compile Include="UIPrincipal\\Documentos\\FrmVistaPreviaResumen.cs">
      <SubType>Form</SubType>
    </Compile>
    <Compile Include="UIPrincipal\\Documentos\\FrmVistaPreviaResumen.Designer.cs">
      <DependentUpon>FrmVistaPreviaResumen.cs</DependentUpon>
    </Compile>
    <EmbeddedResource Include="UIPrincipal\\Documentos\\FrmVistaPreviaResumen.resx">
      <DependentUpon>FrmVistaPreviaResumen.cs</DependentUpon>
    </EmbeddedResource>
'''
s=s.replace('    <Compile Include="UIPrincipal\\Documentos\\FrmVistaPrevia.cs">',entries+'    <Compile Include="UIPrincipal\\Documentos\\FrmVistaPrevia.cs">',1)
assert 'FrmVistaPreviaResumen.resx' in s
p.write_text(s,encoding='utf-8')
print('Formularios de factura y resumen separados; copia previa:',backup)

using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;
namespace SistemaPedidoRestaurante.UICliente
{
    public partial class FrmMisDatos:Form
    {
        public FrmMisDatos()
        {
            InitializeComponent();btnVolver.Click+=(s,e)=>Close();
            btnGuardar.Click+=(s,e)=>VistaGestion.Ejecutar(this,()=>{new ClientesGestionBLL().Guardar(new Cliente{
                PrimerNombre=txtNombre.Text,SegundoNombre=txtSegundoNombre.Text,PrimerApellido=txtApellido.Text,
                SegundoApellido=txtSegundoApellido.Text,Cedula=txtCedula.Text,Telefono=txtTelefono.Text,Email=txtEmail.Text,
                Genero=cmbGenero.SelectedItem as string},true,true);MessageBox.Show(this,"Datos actualizados.","Mis datos");});
            VistaGestion.Iniciar(this,3,()=>{var c=new ClienteBLL().ObtenerMiPerfil();if(c==null){btnGuardar.Enabled=false;throw new ArgumentException("La cuenta no tiene perfil activo.");}
                txtNombre.Text=c.PrimerNombre;txtSegundoNombre.Text=c.SegundoNombre;txtApellido.Text=c.PrimerApellido;txtSegundoApellido.Text=c.SegundoApellido;
                txtCedula.Text=c.Cedula;txtTelefono.Text=c.Telefono;txtEmail.Text=c.Email;cmbGenero.SelectedItem=c.Genero;});
        }
    }
}

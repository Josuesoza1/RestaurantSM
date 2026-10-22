using System;
using System.Text.RegularExpressions;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public class ClienteValidacion
    {
        public void ValidarRegistro(Cliente cliente, string contrasena, string confirmarContrasena)
        {
            GestionValidacion.Cliente(cliente);
            cliente.Cedula=CamposValidacion.Cedula(cliente.Cedula,true);
            cliente.Email=CamposValidacion.Correo(cliente.Email,true);
            CamposValidacion.Texto(contrasena,255,"contraseña",true);
            if (contrasena != confirmarContrasena)
                throw new ArgumentException("Las contraseñas no coinciden.");
        }
    }
}

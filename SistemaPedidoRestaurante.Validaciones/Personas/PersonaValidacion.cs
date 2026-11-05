using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class PersonaValidacion
    {
        public static void Validar(Persona persona, bool cedulaObligatoria, bool telefonoObligatorio, bool correoObligatorio)
        {
            if (persona == null)
            {
                throw new ArgumentException("Ingrese los datos de la persona.");
            }
            persona.PrimerNombre = CamposValidacion.NombrePersona(persona.PrimerNombre, "primer nombre", true);
            persona.SegundoNombre = CamposValidacion.NombrePersona(persona.SegundoNombre, "segundo nombre", false);
            persona.PrimerApellido = CamposValidacion.NombrePersona(persona.PrimerApellido, "primer apellido", true);
            persona.SegundoApellido = CamposValidacion.NombrePersona(persona.SegundoApellido, "segundo apellido", false);
            persona.Cedula = CamposValidacion.Cedula(persona.Cedula, cedulaObligatoria);
            persona.Telefono = CamposValidacion.Telefono(persona.Telefono, telefonoObligatorio);
            persona.Email = CamposValidacion.Correo(persona.Email, correoObligatorio);
            if (persona.Genero != "M" && persona.Genero != "F" && persona.Genero != "O")
            {
                throw new ArgumentException("Seleccione el género.");
            }
        }
    }
}

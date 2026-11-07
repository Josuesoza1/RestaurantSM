using System;
using System.IO;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal static class ArchivoExportado
    {
        internal static void Guardar(string archivo, Action<string> generar)
        {
            string destino = Path.GetFullPath(archivo);
            string temporal = Path.Combine(Path.GetDirectoryName(destino),
                "." + Guid.NewGuid().ToString("N") + Path.GetExtension(destino));
            try
            {
                generar(temporal);
                if (File.Exists(destino))
                    File.Replace(temporal, destino, null);
                else
                    File.Move(temporal, destino);
            }
            finally
            {
                if (File.Exists(temporal))
                    File.Delete(temporal);
            }
        }
    }
}

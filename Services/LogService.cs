using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TechShieldSolution_CyberShield.Services
{
    public class LogService
    {

        public async Task<List<string>> LeerLogAsincrono(string rutaArchivo)
        {
            var lineas = new List<string>();

            if (string.IsNullOrWhiteSpace(rutaArchivo) || !File.Exists(rutaArchivo))
            {

                throw new FileNotFoundException("El Archivo no existe o la ruta es inválida.");

            }

            try
            {

                // Abrir Stream de Forma Segura
                using var stream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var reader = new StreamReader(stream);

                // Leer el Archivo por Cada Linea de Forma Asincrona
                string? linea;
                while ((linea = await reader.ReadLineAsync()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        lineas.Add(linea);
                    }
                }

            }
            catch(Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine($"LogService Error: {ex.Message}");
                throw;
            }

            return lineas;

        }

    }

}

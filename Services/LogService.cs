using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
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


            }
            catch(Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine($"LogService Error: {ex.Message}");
                throw;
            }

            return lineas;

        }

        public string ObternerBootstrapColor(string lineaLog)
        {
            if (lineaLog.Contains("[ERROR]", StringComparison.OrdinalIgnoreCase)) return "text-danger fw-bold";
            if (lineaLog.Contains("[WARN]", StringComparison.OrdinalIgnoreCase)) return "text-warning";
            if (lineaLog.Contains("[INFO]", StringComparison.OrdinalIgnoreCase)) return "text-success";
            return "text-secondary";
        }

    }

}

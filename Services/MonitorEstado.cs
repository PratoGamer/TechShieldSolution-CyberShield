using System;
using System.Collections.Generic;
using System.Text;

namespace TechShieldSolution_CyberShield.Services
{
    public class MonitorEstado
    {
        // Variables a Implementar
        public List<string> listaIncidencias { get; private set; } = new List<string>();
        public bool monitoreoActivo { get; set; } = false;

        // Codigos de Errores Comunes para Leer
        public string[] codigosErrores { get; set; } = new string[]
            {
            "ERR-404", 
            "ERR-500", 
            "ERR-503",
            "ERR_AUTH_FAIL",
            "ERR_SQL_INJECTION",
            "ERR_PORT_SCAN",
            "ERR_OVERFLOW"
            };

        // Diccionario para Guardar Cantidades de Errores
        public Dictionary<string, int> contadorErrores { get; set; } = new Dictionary<string, int>();


        // Notificar Cambios a los Componentes
        public event Action? onStateChanged;

        // Constructor de la Clase MonitorEstado
        public MonitorEstado()
        {
            // Inicializar el Contador de Errores
            foreach (var codigo in codigosErrores)
            {
                contadorErrores[codigo] = 0;
            }
        }

        // Actualizar el Contador de Errores
        public async Task ActualizarContador(string codigo, int cantidad)
        {
            // Verificar que el Codigo Exista
            if (contadorErrores.ContainsKey(codigo))
            {
                contadorErrores[codigo] = cantidad;
            }
        }

        // Agregar Alertas
        public async Task AgregarAlertaAsincrono(string nuevaAlerta)
        {
            // Veririficar que no Hayan Duplicados
            if (!listaIncidencias.Contains(nuevaAlerta))
            {
                listaIncidencias.Add(nuevaAlerta);
                NotificarCambioEstado();
            }

        }

        // Evitar que se Escriba el Mismo Codigo de Error
        public async Task<bool> Reportado(string codigo)
        {
            return listaIncidencias.Any(alerta => alerta.Contains(codigo, StringComparison.OrdinalIgnoreCase));
        }

        // Limpiar Todos los Datos
        public async Task LimpiarIncidenciasAsincrono()
        {
            listaIncidencias.Clear();
            NotificarCambioEstado();
            foreach (var codigo in codigosErrores)
            {
                contadorErrores[codigo] = 0;
            }
        }

        // Notificar el Cambio de Estado para los Componentes
        public void NotificarCambioEstado()
        {
            onStateChanged?.Invoke();
        }

    }
}

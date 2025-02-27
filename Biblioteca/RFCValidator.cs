using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Biblioteca
{
    /// <summary>
    /// Clase para validar y corregir RFCs.
    /// </summary>
    public static class RFCValidator
    {
        /// <summary>
        /// Valida si el RFC tiene el formato correcto.
        /// </summary>
        /// <param name="rfc">RFC a validar.</param>
        /// <returns>True si el RFC es válido, False si no.</returns>
        public static bool EsRFCValido(string rfc)
        {
            string patron = @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{2,3}$";
            return Regex.IsMatch(rfc.ToUpper(), patron);
        }

        /// <summary>
        /// Corrige errores comunes en la captura del RFC.
        /// </summary>
        /// <param name="rfc">RFC a corregir.</param>
        /// <returns>RFC corregido en mayúsculas y sin espacios.</returns>
        public static string CorregirRFC(string rfc)
        {
            return rfc.ToUpper().Trim();
        }
    }
}

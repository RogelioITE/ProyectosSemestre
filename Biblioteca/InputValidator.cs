using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    /// <summary>
    /// Clase para validar entradas de texto.
    /// </summary>
    public static class InputValidator
    {
        /// <summary>
        /// Valida si una cadena contiene solo números.
        /// </summary>
        /// <param name="texto">Texto a validar.</param>
        /// <returns>True si solo contiene números, False si no.</returns>
        public static bool EsSoloNumeros(string texto)
        {
            return texto.All(char.IsDigit);
        }

        /// <summary>
        /// Valida si una cadena contiene solo letras.
        /// </summary>
        /// <param name="texto">Texto a validar.</param>
        /// <returns>True si solo contiene letras, False si no.</returns>
        public static bool EsSoloLetras(string texto)
        {
            return texto.All(char.IsLetter);
        }
    }
}

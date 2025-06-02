using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace AgendaPersonal.Utils
{
    internal class ConfiguracionApp
    {
        public static void GuardarTema(bool esOscuro)
        {
            Preferences.Set("temaOscuro", esOscuro);
        }

        public static bool ObtenerTema()
        {
            return Preferences.Get("temaOscuro", false);
        }
        public static void GuardarIdioma(string idioma)
        {
            Preferences.Set("idioma", idioma);
        }

        public static string ObtenerIdioma()
        {
            return Preferences.Get("idioma", "es");
        }
    }
}

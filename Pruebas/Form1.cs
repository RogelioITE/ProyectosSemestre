using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Biblioteca;

namespace Pruebas
{
    public partial class Form1 : Form
    {
        private CajitaTexto txtRFC;
        private ElBoton btnValidarRFC;

        public Form1()
        {
            InitializeComponent();
            InicializarControles();
        }

        private void InicializarControles()
        {
            // Caja de texto para RFC
            txtRFC = new CajitaTexto
            {
                Location = new System.Drawing.Point(50, 50),
                Size = new System.Drawing.Size(200, 30),
                TipoDeEntrada = CajitaTexto.TipoEntrada.Ambos // RFC permite letras y números
            };

            // Botón para validar RFC
            btnValidarRFC = new ElBoton
            {
                Location = new System.Drawing.Point(50, 100),
                Size = new System.Drawing.Size(120, 40),
                TextoBoton = "Validar RFC",
                RFCTextBox = txtRFC.Controls[0] as TextBox // Enlazar el TextBox de CajitaTexto
            };

            // Agregar controles al formulario
            Controls.Add(txtRFC);
            Controls.Add(btnValidarRFC);
        }
    }
}

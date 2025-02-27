using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class CajitaTexto : UserControl
    {
        private TextBox textBox;
        public enum TipoEntrada { Numeros, Letras, Ambos }
        public TipoEntrada TipoDeEntrada { get; set; } = TipoEntrada.Ambos;

        public CajitaTexto()
        {
            InitializeComponent();
            InicializarTextBox();
        }

        private void InicializarTextBox()
        {
            textBox = new TextBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            textBox.KeyPress += TextBox_KeyPress;
            textBox.TextChanged += TextBox_TextChanged; // Nuevo evento para validar mientras se escribe

            Controls.Add(textBox);
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool invalido = (TipoDeEntrada == TipoEntrada.Numeros && !InputValidator.EsSoloNumeros(e.KeyChar.ToString())) ||
                            (TipoDeEntrada == TipoEntrada.Letras && !InputValidator.EsSoloLetras(e.KeyChar.ToString()));

            e.Handled = invalido;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Validación en tiempo real con InputValidator
            if ((TipoDeEntrada == TipoEntrada.Numeros && !InputValidator.EsSoloNumeros(textBox.Text)) ||
                (TipoDeEntrada == TipoEntrada.Letras && !InputValidator.EsSoloLetras(textBox.Text)))
            {
                textBox.BackColor = Color.LightCoral;
            }
            else
            {
                textBox.BackColor = Color.White;
            }
        }

        public string Texto
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }
    }
}
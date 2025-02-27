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
    public partial class ElBoton : UserControl
    {
        private Button button;
        public TextBox RFCTextBox { get; set; } // Campo a validar

        public ElBoton()
        {
            InitializeComponent();
            InicializarBoton();
        }

        private void InicializarBoton()
        {
            button = new Button
            {
                Text = "Validar RFC",
                Dock = DockStyle.Fill,
                BackColor = Color.LightGray
            };

            button.MouseEnter += (s, e) => button.BackColor = Color.LightBlue;
            button.MouseLeave += (s, e) => button.BackColor = Color.LightGray;
            button.Click += Button_Click;

            Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (RFCTextBox == null || string.IsNullOrWhiteSpace(RFCTextBox.Text))
            {
                MessageBox.Show("Por favor, ingrese un RFC.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rfc = RFCValidator.CorregirRFC(RFCTextBox.Text);

            if (RFCValidator.EsRFCValido(rfc))
            {
                MessageBox.Show("RFC válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("RFC inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string TextoBoton
        {
            get { return button.Text; }
            set { button.Text = value; }
        }
    }
}
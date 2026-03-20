using System;
using System.Globalization;
using System.Windows.Forms;

namespace Interface_form_
{
    public partial class SafetySettingsForm : Form
    {
        public double CycleTime { get; private set; }
        public double SecurityDistance { get; private set; }

        public SafetySettingsForm()
        {
            InitializeComponent();

            if (cancelButton != null)
                cancelButton.Click += cancelButton_Click;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            // Valida y guarda la configuración mínima necesaria para simular.
            if (!TryParsePositiveDouble(cyclebox.Text, out double cycleTime) ||
                !TryParsePositiveDouble(securitybox.Text, out double securityDistance))
            {
                MessageBox.Show("El tiempo de ciclo y la distancia de seguridad deben ser valores numéricos mayores que 0.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CycleTime = cycleTime;
            SecurityDistance = securityDistance;

            DialogResult = DialogResult.OK;
            Close();
        }

        private static bool TryParsePositiveDouble(string text, out double value)
        {
            // Acepta formato local o invariante, pero exige valores mayores que cero.
            return (double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value) ||
                    double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                   && value > 0;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

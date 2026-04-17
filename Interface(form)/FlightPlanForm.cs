using System;
using System.Globalization;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    public partial class FlightPlanForm : Form
    {
        // La lista se recibe desde Main para modificar directamente el estado compartido.
        public FlightPlanList _flightplans;

        public FlightPlan Flightplan1 { get; set; }
        public FlightPlan Flightplan2 { get; set; }

        // ── Constructor ──────────────────────────────────────────────────────────

        public FlightPlanForm(FlightPlanList flightplans)
        {
            InitializeComponent();
            _flightplans = flightplans;

            if (cancelButton != null)
                cancelButton.Click += CancelButton_Click;
        }

        // ── Botón Accept ─────────────────────────────────────────────────────────

        private void acceptButton_Click(object sender, EventArgs e)
        {
            // Leer y validar los identificadores.
            string id1 = id1box.Text.Trim();
            string id2 = id2box.Text.Trim();

            if (string.IsNullOrWhiteSpace(id1) || string.IsNullOrWhiteSpace(id2))
            {
                MessageBox.Show("Los identificadores de los dos planes son obligatorios.",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (id1.Equals(id2, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Los ID de los planes deben ser distintos.",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Leer las compañías (campo opcional).
            string company1 = company1box.Text.Trim();
            string company2 = company2box.Text.Trim();
            if (string.IsNullOrEmpty(company1)) company1 = "Unknown";
            if (string.IsNullOrEmpty(company2)) company2 = "Unknown";

            // Parsear cada coordenada por separado.
            // Se inicializan a 0 para evitar CS0165 (variable no asignada).
            double o1x = 0, o1y = 0, d1x = 0, d1y = 0, velocity1 = 0;
            double o2x = 0, o2y = 0, d2x = 0, d2y = 0, velocity2 = 0;

            bool ok1 =
                TryParseDouble(origin1Xbox.Text,  out o1x)   &&
                TryParseDouble(origin1Ybox.Text,  out o1y)   &&
                TryParseDouble(dest1Xbox.Text,    out d1x)   &&
                TryParseDouble(dest1Ybox.Text,    out d1y)   &&
                TryParsePositiveDouble(velocity1box.Text, out velocity1);

            bool ok2 =
                TryParseDouble(origin2Xbox.Text,  out o2x)   &&
                TryParseDouble(origin2Ybox.Text,  out o2y)   &&
                TryParseDouble(dest2Xbox.Text,    out d2x)   &&
                TryParseDouble(dest2Ybox.Text,    out d2y)   &&
                TryParsePositiveDouble(velocity2box.Text, out velocity2);

            if (!ok1 || !ok2)
            {
                MessageBox.Show(
                    "Revise los valores numéricos.\n" +
                    "- Las coordenadas X e Y deben ser números.\n" +
                    "- La velocidad debe ser mayor que 0.\n" +
                    "- Rango recomendado del panel: X 0-1200, Y 0-900.",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Construir los vuelos con coordenadas separadas.
            Flightplan1 = new FlightPlan(id1, company1, o1x, o1y, d1x, d1y, velocity1);
            Flightplan2 = new FlightPlan(id2, company2, o2x, o2y, d2x, d2y, velocity2);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Helpers de parseo ────────────────────────────────────────────────────

        private static bool TryParsePositiveDouble(string text, out double value)
        {
            return TryParseDouble(text, out value) && value > 0;
        }

        private static bool TryParseDouble(string text, out double value)
        {
            // Acepta tanto coma como punto como separador decimal.
            string normalized = text.Trim().Replace(',', '.');
            return double.TryParse(normalized, NumberStyles.Float,
                CultureInfo.InvariantCulture, out value);
        }

        // ── Botón Cancel ─────────────────────────────────────────────────────────

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ── Botón Sin conflicto (vuelo de prueba rápido) ─────────────────────────

        private void nonConflictbtn_Click(object sender, EventArgs e)
        {
            // Dos vuelos paralelos que no se cruzan: no generan conflicto.
            Flightplan1 = new FlightPlan("IBE001", "Iberia",  100, 300, 1100, 300, 100);
            Flightplan2 = new FlightPlan("VLG002", "Vueling", 100, 600, 1100, 600, 100);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Botón Con conflicto (vuelo de prueba rápido) ─────────────────────────

        private void conflictbtn_Click(object sender, EventArgs e)
        {
            // Vuelo horizontal y vuelo vertical que se cruzan en (600, 450).
            Flightplan1 = new FlightPlan("IBE001", "Iberia",  100, 450, 1100, 450, 100);
            Flightplan2 = new FlightPlan("VLG002", "Vueling", 600, 800,  600, 100, 100);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Botón Reset ───────────────────────────────────────────────────────────

        private void resetbtn_Click(object sender, EventArgs e)
        {
            _flightplans.Clear();
            MessageBox.Show("La lista de planes de vuelo ha sido vaciada.",
                "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

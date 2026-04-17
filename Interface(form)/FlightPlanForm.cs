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

        // Los planes creados quedan accesibles para quien abra este formulario.
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

            // Leer las compañías (campo opcional — si está vacío se asigna "Unknown").
            string company1 = company1box.Text.Trim();
            string company2 = company2box.Text.Trim();
            if (string.IsNullOrEmpty(company1)) company1 = "Unknown";
            if (string.IsNullOrEmpty(company2)) company2 = "Unknown";

            // Parsear coordenadas y velocidades.
            double o1x, o1y, d1x, d1y, velocity1;
            double o2x, o2y, d2x, d2y, velocity2;

            if (!TryParsePoint(origin1box.Text, out o1x, out o1y)           ||
                !TryParsePoint(destination1box.Text, out d1x, out d1y)      ||
                !TryParsePositiveDouble(velocity1box.Text, out velocity1)   ||
                !TryParsePoint(origin2box.Text, out o2x, out o2y)           ||
                !TryParsePoint(destination2box.Text, out d2x, out d2y)      ||
                !TryParsePositiveDouble(velocity2box.Text, out velocity2))
            {
                MessageBox.Show(
                    "Revise el formato de entrada.\n" +
                    "- Origen y destino deben tener formato X,Y\n" +
                    "- Velocidad debe ser numérica y mayor que 0.",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Construir los vuelos con el nuevo constructor que incluye compañía.
            Flightplan1 = new FlightPlan(id1, company1, o1x, o1y, d1x, d1y, velocity1);
            Flightplan2 = new FlightPlan(id2, company2, o2x, o2y, d2x, d2y, velocity2);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Helpers de parseo ────────────────────────────────────────────────────

        private static bool TryParsePoint(string text, out double x, out double y)
        {
            x = 0;
            y = 0;
            string[] values = text.Split(',');
            if (values.Length != 2) return false;
            return TryParseDouble(values[0].Trim(), out x) &&
                   TryParseDouble(values[1].Trim(), out y);
        }

        private static bool TryParsePositiveDouble(string text, out double value)
        {
            return TryParseDouble(text, out value) && value > 0;
        }

        private static bool TryParseDouble(string text, out double value)
        {
            // Acepta separador decimal regional e invariante.
            return double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value) ||
                   double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        // ── Botón Cancel ─────────────────────────────────────────────────────────

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ── Botón NonConflict (caso de prueba sin conflicto) ──────────────────

        private void nonConflictbtn_Click(object sender, EventArgs e)
        {
            // Caso de prueba rápido que no genera conflicto.
            Flightplan1 = new FlightPlan("FP-A100", "AirlineA", 0.0,   0.0, 500.0, 1000.0, 500.0);
            Flightplan2 = new FlightPlan("FP-B200", "AirlineB", 500.0, 500.0, 0.0,    0.0, 600.0);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Botón Conflict (caso de prueba con conflicto) ─────────────────────

        private void conflictbtn_Click(object sender, EventArgs e)
        {
            // Caso de prueba rápido que genera conflicto.
            Flightplan1 = new FlightPlan("FP-A100", "AirlineA", 0.0,   0.0, 500.0, 500.0, 500.0);
            Flightplan2 = new FlightPlan("FP-B200", "AirlineB", 500.0, 0.0,   0.0, 500.0, 500.0);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Botón Reset ───────────────────────────────────────────────────────

        private void resetbtn_Click(object sender, EventArgs e)
        {
            _flightplans.Clear();
            MessageBox.Show("La lista de planes de vuelo ha sido vaciada.",
                "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

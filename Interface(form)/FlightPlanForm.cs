using System;
using System.Globalization;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    public partial class FlightPlanForm : Form
    {
        public FlightPlanList _flightplans;

        public FlightPlan Flightplan1 { get; set; }
        public FlightPlan Flightplan2 { get; set; }

        public FlightPlanForm(FlightPlanList flightplans)
        {
            InitializeComponent();
            _flightplans = flightplans;

            if (cancelButton != null)
                cancelButton.Click += CancelButton_Click;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
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

            string company1 = company1box.Text.Trim();
            string company2 = company2box.Text.Trim();
            if (string.IsNullOrEmpty(company1)) company1 = "Unknown";
            if (string.IsNullOrEmpty(company2)) company2 = "Unknown";

            // Inicializar a 0 para evitar CS0165 — si el parseo falla, ok será false
            // y se devuelve error antes de usar los valores.
            double o1x = 0, o1y = 0, d1x = 0, d1y = 0, velocity1 = 0;
            double o2x = 0, o2y = 0, d2x = 0, d2y = 0, velocity2 = 0;

            if (!TryParsePoint(origin1box.Text,      out o1x, out o1y)    ||
                !TryParsePoint(destination1box.Text, out d1x, out d1y)    ||
                !TryParseVelocity(velocity1box.Text, out velocity1)       ||
                !TryParsePoint(origin2box.Text,      out o2x, out o2y)    ||
                !TryParsePoint(destination2box.Text, out d2x, out d2y)    ||
                !TryParseVelocity(velocity2box.Text, out velocity2))
            {
                MessageBox.Show(
                    "Revise los valores introducidos:\n\n" +
                    "· Origen y Destino: formato  X,Y\n" +
                    "  X debe estar entre 0 y 1200\n" +
                    "  Y debe estar entre 0 y 900\n" +
                    "  Ejemplo válido: 100,450\n\n" +
                    "· Velocidad: número entre 1 y 1000 (u/min)",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Flightplan1 = new FlightPlan(id1, company1, o1x, o1y, d1x, d1y, velocity1);
            Flightplan2 = new FlightPlan(id2, company2, o2x, o2y, d2x, d2y, velocity2);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Helpers de parseo y validación ──────────────────────────────────────

        // Límites del espacio de simulación (deben coincidir con panel1.Size en SimulationForm).
        private const double PANEL_W = 1200;
        private const double PANEL_H = 900;
        private const double VEL_MAX = 1000;

        /// <summary>
        /// Parsea "X,Y" y valida que ambas coordenadas estén dentro del panel.
        /// </summary>
        private static bool TryParsePoint(string text, out double x, out double y)
        {
            x = 0; y = 0;
            string[] values = text.Split(',');
            if (values.Length != 2) return false;
            if (!TryParseDouble(values[0].Trim(), out x)) return false;
            if (!TryParseDouble(values[1].Trim(), out y)) return false;
            // Comprobar que las coordenadas estén dentro del área visible del panel.
            return x >= 0 && x <= PANEL_W && y >= 0 && y <= PANEL_H;
        }

        /// <summary>
        /// Valida que la velocidad sea un número entre 1 y VEL_MAX (1000).
        /// </summary>
        private static bool TryParseVelocity(string text, out double value)
        {
            if (!TryParseDouble(text, out value)) return false;
            return value > 0 && value <= VEL_MAX;
        }

        private static bool TryParseDouble(string text, out double value)
        {
            // Normalizar coma → punto para aceptar ambos separadores decimales.
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

        // ── Botones de prueba rápida ──────────────────────────────────────────────

        private void nonConflictbtn_Click(object sender, EventArgs e)
        {
            // Dos vuelos horizontales paralelos — nunca se cruzan.
            Flightplan1 = new FlightPlan("IBE001", "Iberia",  100, 300, 1100, 300, 300);
            Flightplan2 = new FlightPlan("VLG002", "Vueling", 100, 600, 1100, 600, 300);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void conflictbtn_Click(object sender, EventArgs e)
        {
            // IBE001 horizontal + VLG002 vertical — se cruzan en (600, 450).
            // Velocidades proporcionales a la distancia al cruce para que lleguen al mismo tiempo:
            //   IBE001: 500 unidades al cruce → velocidad 500
            //   VLG002: 350 unidades al cruce → velocidad 350
            // Ambos llegan en exactamente 60 ciclos (cycleTime=1 min).
            Flightplan1 = new FlightPlan("IBE001", "Iberia",  100, 450, 1100, 450, 500);
            Flightplan2 = new FlightPlan("VLG002", "Vueling", 600, 800,  600, 100, 350);

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

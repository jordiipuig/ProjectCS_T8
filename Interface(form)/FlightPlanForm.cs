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
                MessageBox.Show("Los identificadores de los dos planes son obligatorios.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (id1.Equals(id2, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Los ID de los planes deben ser distintos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryParsePoint(origin1box.Text, out double o1x, out double o1y) ||
                !TryParsePoint(destination1box.Text, out double d1x, out double d1y) ||
                !TryParsePositiveDouble(velocity1box.Text, out double velocity1) ||
                !TryParsePoint(origin2box.Text, out double o2x, out double o2y) ||
                !TryParsePoint(destination2box.Text, out double d2x, out double d2y) ||
                !TryParsePositiveDouble(velocity2box.Text, out double velocity2))
            {
                MessageBox.Show("Revise el formato de entrada.\n- Origen y destino deben tener formato X,Y\n- Velocidad debe ser numérica y mayor que 0.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Flightplan1 = new FlightPlan(id1, o1x, o1y, d1x, d1y, velocity1);
            Flightplan2 = new FlightPlan(id2, o2x, o2y, d2x, d2y, velocity2);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        private static bool TryParsePoint(string text, out double x, out double y)
        {
            x = 0;
            y = 0;

            string[] values = text.Split(',');
            if (values.Length != 2)
            {
                return false;
            }

            return TryParseDouble(values[0].Trim(), out x) && TryParseDouble(values[1].Trim(), out y);
        }

        private static bool TryParsePositiveDouble(string text, out double value)
        {
            return TryParseDouble(text, out value) && value > 0;
        }

        private static bool TryParseDouble(string text, out double value)
        {
            return double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value) ||
                   double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void nonConflictbtn_Click(object sender, EventArgs e)
        {
            string id1 = "FP-A100";
            string id2 = "FP-B200";

            double o1x = 0.0;
            double o1y = 0.0;
            double d1x = 500.0;
            double d1y = 1000.0;
            double v1 = 500.0;

            double o2x = 500.0;
            double o2y = 500;
            double d2x = 0.0;
            double d2y = 0.0;
            double v2 = 600.0;

            Flightplan1 = new FlightPlan(id1, o1x, o1y, d1x, d1y, v1);
            Flightplan2 = new FlightPlan(id2, o2x, o2y, d2x, d2y, v2);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void conflictbtn_Click(object sender, EventArgs e)
        {
            string id1 = "FP-A100";
            string id2 = "FP-B200";

            double o1x = 0.0;
            double o1y = 0.0;
            double d1x = 500.0;
            double d1y = 500;
            double v1 = 500.0;

            double o2x = 500.0;
            double o2y = 0;
            double d2x = 0.0;
            double d2y = 500;
            double v2 = 500;

            Flightplan1 = new FlightPlan(id1, o1x, o1y, d1x, d1y, v1);
            Flightplan2 = new FlightPlan(id2, o2x, o2y, d2x, d2y, v2);

            _flightplans.Clear();
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            _flightplans.Clear();
            MessageBox.Show("La lista de planes de vuelo ha sido vaciada.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

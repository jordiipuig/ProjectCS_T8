using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;
using System.Globalization;

namespace Interface_form_
{
    public partial class FlightGrid : Form
    {
        private readonly FlightPlanList flightplans;
        private readonly Button applySpeedsButton;
        public bool SpeedsUpdated { get; private set; }

        public FlightGrid(FlightPlanList _flightPlans)
        {
            InitializeComponent();
            flightplans = _flightPlans;

            // Mantiene la consulta clásica mediante selección manual de dos filas.
            acceptbtn.Click += Acceptbtn_Click;
            // Al pulsar una fila se abre el detalle de distancia del vuelo seleccionado.
            Finfo.CellClick += Finfo_CellClick;

            // Botón adicional para aplicar velocidades editadas y cerrar el formulario.
            applySpeedsButton = new Button
            {
                Name = "applySpeedsButton",
                Text = "Apply Speeds + Restart",
                Size = new Size(200, 47),
                Location = new Point(654, 195),
                UseVisualStyleBackColor = true
            };
            applySpeedsButton.Click += ApplySpeedsButton_Click;
            Controls.Add(applySpeedsButton);
        }

        private void FlightGrid_Load(object sender, EventArgs e)
        {
            // Configura las columnas del DataGridView que resume el estado actual.
            Finfo.Columns.Clear();
            Finfo.Rows.Clear();
            Finfo.ColumnCount = 3;
            Finfo.Columns[0].Name = "ID";
            Finfo.Columns[1].Name = "Posición Actual u";
            Finfo.Columns[2].Name = "Velocidad u/s";
            Finfo.Columns[0].ReadOnly = true;
            Finfo.Columns[1].ReadOnly = true;
            Finfo.Columns[2].ReadOnly = false;

            // Llena el DataGridView con el estado de cada vuelo disponible.
            int numFlights = flightplans.getnum();
            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan plan = flightplans.GetFlightPlan(i);
                string id = plan.GetId();
                Position pos = plan.GetCurrentPosition();
                string posStr = $"({pos.GetX():F2}, {pos.GetY():F2})";
                double velocidad = plan.GetVelocidad();

                Finfo.Rows.Add(id, posStr, velocidad);
            }

            Finfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Finfo.ColumnHeadersVisible = true;
            Finfo.RowHeadersVisible = false;
            Finfo.MultiSelect = true;
            Finfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Finfo.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void Finfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si se pulsa la columna velocidad, el usuario quiere editar y no ver distancias.
            if (e.ColumnIndex == 2)
            {
                return;
            }

            if (e.RowIndex < 0 || flightplans.getnum() < 2)
            {
                return;
            }

            FlightPlan selectedFlight = flightplans.GetFlightPlan(e.RowIndex);
            FlightPlan otherFlight = GetOtherFlight(e.RowIndex);

            if (selectedFlight == null || otherFlight == null)
            {
                return;
            }

            using (FlightDistanceForm form = new FlightDistanceForm(selectedFlight, otherFlight))
            {
                form.ShowDialog(this);
            }
        }

        private void Acceptbtn_Click(object sender, EventArgs e)
        {
            if (Finfo.SelectedRows.Count == 2)
            {
                int index1 = Finfo.SelectedRows[0].Index;
                int index2 = Finfo.SelectedRows[1].Index;

                FlightPlan plan1 = flightplans.GetFlightPlan(index1);
                FlightPlan plan2 = flightplans.GetFlightPlan(index2);

                double distance = plan1.Distance(plan2);
                distancebox.Text = distance.ToString("F2");
            }
            else
            {
                distancebox.Text = "Seleccione dos vuelos";
            }
        }

        private FlightPlan GetOtherFlight(int selectedIndex)
        {
            for (int i = 0; i < flightplans.getnum(); i++)
            {
                if (i != selectedIndex)
                {
                    return flightplans.GetFlightPlan(i);
                }
            }

            return null;
        }

        private void ApplySpeedsButton_Click(object sender, EventArgs e)
        {
            // Confirma la edición en curso del DataGridView antes de validar.
            Finfo.EndEdit();

            for (int i = 0; i < flightplans.getnum(); i++)
            {
                DataGridViewCell speedCell = Finfo.Rows[i].Cells[2];
                string speedText = Convert.ToString(speedCell.Value);

                if (!TryParsePositiveDouble(speedText, out double speed))
                {
                    MessageBox.Show(
                        $"La velocidad de la fila {i + 1} no es válida. Introduce un valor numérico mayor que 0.",
                        "Error de validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Actualiza el modelo compartido para que la simulación use los nuevos valores.
                flightplans.GetFlightPlan(i).SetVelocidad(speed);
                speedCell.Value = speed.ToString("F2");
            }

            SpeedsUpdated = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private static bool TryParsePositiveDouble(string text, out double value)
        {
            return (double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value) ||
                    double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                   && value > 0;
        }
    }
}

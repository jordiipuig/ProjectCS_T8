using System;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    public partial class FlightGrid : Form
    {
        private readonly FlightPlanList flightplans;

        public FlightGrid(FlightPlanList _flightPlans)
        {
            InitializeComponent();
            flightplans = _flightPlans;

            // Botón Accept — calcula la distancia entre las dos filas seleccionadas.
            acceptbtn.Click += Acceptbtn_Click;

            // Clic en una fila — abre el detalle de distancia con el vuelo más cercano.
            Finfo.CellClick += Finfo_CellClick;
        }

        private void FlightGrid_Load(object sender, EventArgs e)
        {
            // Configurar las columnas del DataGridView (v2: 4 columnas con Empresa).
            Finfo.Columns.Clear();
            Finfo.Rows.Clear();
            Finfo.ColumnCount = 4;
            Finfo.Columns[0].Name = "ID";
            Finfo.Columns[1].Name = "Empresa";          // nuevo en v2
            Finfo.Columns[2].Name = "Posición Actual u";
            Finfo.Columns[3].Name = "Velocidad u/s";

            // Llenar el DataGridView con el estado actual de cada vuelo.
            int numFlights = flightplans.getnum();
            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan plan   = flightplans.GetFlightPlan(i);
                string id         = plan.GetId();
                string company    = plan.GetCompany();  // nuevo en v2
                Position pos      = plan.GetCurrentPosition();
                string posStr     = "(" + pos.GetX().ToString("F2") + ", " + pos.GetY().ToString("F2") + ")";
                double velocidad  = plan.GetVelocidad();

                Finfo.Rows.Add(id, company, posStr, velocidad);
            }

            Finfo.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.AllCells;
            Finfo.ColumnHeadersVisible  = true;
            Finfo.RowHeadersVisible     = false;
            Finfo.MultiSelect           = true;
            Finfo.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Finfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || flightplans.getnum() < 2)
            {
                return;
            }

            FlightPlan selectedFlight = flightplans.GetFlightPlan(e.RowIndex);
            FlightPlan otherFlight    = GetOtherFlight(e.RowIndex);

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

        /// <summary>
        /// Devuelve el primer vuelo de la lista distinto del índice indicado.
        /// </summary>
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
    }
}

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

namespace Interface_form_
{
    public partial class FlightGrid : Form
    {
        private readonly FlightPlanList flightplans;

        public FlightGrid(FlightPlanList _flightPlans)
        {
            InitializeComponent();
            flightplans = _flightPlans;

            // Mantiene la consulta clásica mediante selección manual de dos filas.
            acceptbtn.Click += Acceptbtn_Click;
            // Al pulsar una fila se abre el detalle de distancia del vuelo seleccionado.
            Finfo.CellClick += Finfo_CellClick;
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
        }

        private void Finfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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
    }
}

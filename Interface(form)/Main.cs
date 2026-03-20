using FlightLib;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Interface_form_
{
    public partial class Main : Form
    {
        // Estado compartido entre los formularios secundarios.
        private FlightPlanList flightPlans = new FlightPlanList();
        private double securityDistance = 50;
        private double cycleTime = 1;

        public Main()
        {
            InitializeComponent();
        }

        private void flightPlansToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Abre el formulario que crea o reemplaza los vuelos cargados.
            FlightPlanForm form = new FlightPlanForm(flightPlans);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                flightPlans.EscribeConsola();
                MessageBox.Show("Flight plans added to Main.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void securitySettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SafetySettingsForm form = new SafetySettingsForm();
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // Lee y conserva la configuración activa de la simulación.
                    securityDistance = form.SecurityDistance;
                    cycleTime = form.CycleTime;

                    MessageBox.Show($"Security distance set to {securityDistance}\nCycle time set to {cycleTime}", "Safety Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Verifica que existen vuelos suficientes antes de abrir la simulación.
            FlightPlan fp1 = flightPlans.GetFlightPlan(0);
            FlightPlan fp2 = flightPlans.GetFlightPlan(1);

            if (fp1 == null || fp2 == null)
            {
                MessageBox.Show("Debe añadir al menos dos planes de vuelo antes de iniciar la simulación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Abre el formulario principal de simulación con la configuración actual.
            SimulationForm form = new SimulationForm(flightPlans, cycleTime, securityDistance);
            form.ShowDialog(this);
           
        }

    }
}

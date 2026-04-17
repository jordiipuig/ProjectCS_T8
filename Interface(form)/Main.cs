using FlightLib;
using System;
using System.Windows.Forms;

namespace Interface_form_
{
    public partial class Main : Form
    {
        // ── Estado compartido entre formularios secundarios ──────────────────────

        private FlightPlanList flightPlans = new FlightPlanList();
        private double securityDistance = 50;
        private double cycleTime = 1;

        // Nombre del usuario autenticado que abrió la sesión.
        private string _username;

        // ── Constructores ────────────────────────────────────────────────────────

        /// <summary>
        /// Constructor v2: recibe el nombre del usuario que inició sesión.
        /// </summary>
        public Main(string username)
        {
            InitializeComponent();
            _username = username;

            // Mostrar el nombre de bienvenida en la etiqueta del formulario.
            if (lblWelcome != null)
            {
                lblWelcome.Text = "Bienvenido, " + _username;
            }
        }

        /// <summary>
        /// Constructor por defecto — mantiene compatibilidad con código existente.
        /// </summary>
        public Main()
        {
            InitializeComponent();
            _username = string.Empty;
        }

        // ── Manejadores del menú Options ─────────────────────────────────────────

        private void flightPlansToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Abre el formulario que crea o reemplaza los vuelos cargados.
            FlightPlanForm form = new FlightPlanForm(flightPlans);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                flightPlans.EscribeConsola();
                MessageBox.Show("Planes de vuelo añadidos.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void securitySettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SafetySettingsForm form = new SafetySettingsForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                // Lee y conserva la configuración activa de la simulación.
                securityDistance = form.SecurityDistance;
                cycleTime        = form.CycleTime;

                MessageBox.Show(
                    "Distancia de seguridad: " + securityDistance +
                    "\nTiempo de ciclo: " + cycleTime,
                    "Configuración de seguridad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Carga planes de vuelo desde un archivo CSV (separado por ';').
        /// </summary>
        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir el diálogo de selección de archivo.
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title  = "Cargar planes de vuelo";
            dlg.Filter = "Archivos de vuelo|*.txt;*.csv|Todos|*.*";

            if (dlg.ShowDialog(this) != DialogResult.OK)
            {
                return; // El usuario canceló — no hacer nada.
            }

            string path = dlg.FileName;

            // Intentar cargar los vuelos desde el archivo seleccionado.
            string error = flightPlans.LoadFromFile(path);

            if (error == null)
            {
                // Carga exitosa.
                MessageBox.Show(
                    "Se cargaron " + flightPlans.getnum() + " vuelo(s) desde el archivo.",
                    "Carga correcta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                // Mostrar el mensaje de error devuelto por LoadFromFile.
                MessageBox.Show(
                    "Error al cargar el archivo:\n" + error,
                    "Error de carga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ── Menú Simulation Airspace ─────────────────────────────────────────────

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Verificar que existen al menos dos vuelos cargados.
            if (flightPlans.getnum() < 2)
            {
                MessageBox.Show(
                    "Debe añadir al menos dos planes de vuelo antes de iniciar la simulación.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Abrir el formulario de simulación con la configuración actual.
            SimulationForm form = new SimulationForm(flightPlans, cycleTime, securityDistance);
            form.ShowDialog(this);
        }
    }
}

using System;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    public partial class FlightInfo : Form
    {
        // Vuelo cuya información se mostrará al cargarse el formulario.
        FlightPlan currentFP;

        public FlightInfo()
        {
            InitializeComponent();
        }

        /// <summary>Establece el vuelo a mostrar antes de abrir el formulario.</summary>
        public void setFlight(FlightPlan f)
        {
            this.currentFP = f;
        }

        private void FlightInfo_Load(object sender, EventArgs e)
        {
            // Volcar en pantalla el estado actual del vuelo seleccionado.
            Position pos = currentFP.GetCurrentPosition();
            double x     = pos.GetX();
            double y     = pos.GetY();

            xbox.Text        = x.ToString("F2");
            ybox.Text        = y.ToString("F2");
            Idbox.Text       = currentFP.GetId();
            speedbox.Text    = currentFP.GetVelocidad().ToString("F2");
            companybox.Text  = currentFP.GetCompany(); // nuevo en v2
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

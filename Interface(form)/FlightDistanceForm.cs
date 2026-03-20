using System.Drawing;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    /// <summary>
    /// Ventana sencilla que muestra la distancia actual entre dos vuelos.
    /// </summary>
    public class FlightDistanceForm : Form
    {
        public FlightDistanceForm(FlightPlan selectedFlight, FlightPlan otherFlight)
        {
            Text = "Distancia entre vuelos";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(340, 170);

            double distance = selectedFlight.Distance(otherFlight);

            Label selectedLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold),
                Location = new Point(20, 20),
                Text = $"Vuelo seleccionado: {selectedFlight.GetId()}"
            };

            Label otherLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 10F),
                Location = new Point(20, 60),
                Text = $"Otro vuelo: {otherFlight.GetId()}"
            };

            Label distanceLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 10F),
                Location = new Point(20, 100),
                Text = $"Distancia actual: {distance:F2}"
            };

            Controls.Add(selectedLabel);
            Controls.Add(otherLabel);
            Controls.Add(distanceLabel);
        }
    }
}

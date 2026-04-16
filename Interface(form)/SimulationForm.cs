using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    public partial class SimulationForm : Form
    {
        // Datos compartidos que describen la simulación activa.
        private readonly FlightPlanList _flightPlans;
        private readonly double _cycleTime;
        private readonly double _securityDistance;
        PictureBox[] flights;
        private Timer simulationTimer;

        public SimulationForm(FlightPlanList flightPlans, double cycleTime, double securityDistance)
        {
            InitializeComponent();
            _flightPlans = flightPlans;
            _cycleTime = cycleTime;
            _securityDistance = securityDistance;
            // Bloque preparado para merges: evita suscripciones duplicadas.
            panel1.Paint -= panel1_Paint;
            panel1.Paint += panel1_Paint;

            // Temporizador para ejecutar automáticamente los mismos pasos del botón Cycle.
            simulationTimer = new Timer();
            simulationTimer.Interval = (int)(_cycleTime * 1000); // Convert seconds to milliseconds
            simulationTimer.Tick -= timer1_Tick;
            simulationTimer.Tick += timer1_Tick;
        }

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            // Crea un icono por vuelo y lo sitúa en la posición inicial del plan.
            flights = new PictureBox[_flightPlans.getnum()];
            int i = 0;
            while (i < _flightPlans.getnum())
            {
                PictureBox p = new PictureBox();
                FlightPlan f = _flightPlans.GetFlightPlan(i);

                p.Width = 20;
                p.Height = 20;
                p.ClientSize = new Size(20, 20);

                Position initialPosition = f.GetInitialPosition();
                int x = (int)initialPosition.GetX() - p.Width / 2;
                int y = panel1.Height - (int)initialPosition.GetY() - p.Height / 2;

                p.Location = new Point(x, y);
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                p.Image = CreateFlightMarkerImage();

                p.Tag = i;
                p.Click += new System.EventHandler(this.flightInfo);

                panel1.Controls.Add(p);
                flights[i] = p;
                i++;
            }
            // Envía los iconos al fondo para que las trayectorias y elipses se dibujen encima.
            foreach (Control c in panel1.Controls)
            {
                c.SendToBack();
            }
        }

        // Abre el detalle del vuelo representado por el icono pulsado.
        private void flightInfo(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            int i = (int)p.Tag;
            FlightInfo f = new FlightInfo();
            f.setFlight(_flightPlans.GetFlightPlan(i));
            f.ShowDialog();
        }

        private void cyclebtn_Click(object sender, EventArgs e)
        {
            // Ejecuta un único ciclo manual: mover, refrescar pantalla y revisar conflictos.
            MoveFlightsOneCycle();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Black, 2))
            using (Pen circlePen = new Pen(Color.Blue, 2))
            {
                pen.DashStyle = DashStyle.Dash;
                for (int i = 0; i < _flightPlans.getnum(); i++)
                {
                    FlightPlan flight = _flightPlans.GetFlightPlan(i);
                    Position origin = flight.GetInitialPosition();
                    Position dest = flight.GetFinalPosition();

                    int x1 = (int)origin.GetX();
                    int y1 = panel1.Height - (int)origin.GetY();
                    int x2 = (int)dest.GetX();
                    int y2 = panel1.Height - (int)dest.GetY();

                    e.Graphics.DrawLine(pen, x1, y1, x2, y2);

                    Position current = flight.GetCurrentPosition();
                    float centerX = (float)current.GetX();
                    float centerY = panel1.Height - (float)current.GetY();

                    float radius = (float)_securityDistance;
                    float ellipseX = centerX - radius;
                    float ellipseY = centerY - radius;

                    // Dibuja la distancia de seguridad centrada sobre el avión.
                    e.Graphics.DrawEllipse(circlePen, ellipseX, ellipseY, radius * 2, radius * 2);
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Repite automáticamente el mismo avance que realiza el botón Cycle.
            MoveFlightsOneCycle();
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            // Antes de iniciar la simulación automática, comprueba conflictos previstos.
            int numFlights = _flightPlans.getnum();
            bool conflictPredicted = false;
            int conflictA = -1, conflictB = -1;

            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                for (int j = i + 1; j < numFlights; j++)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);

                    if (WillFlightsConflict(a, b, _securityDistance))
                    {
                        conflictPredicted = true;
                        conflictA = i;
                        conflictB = j;
                        break;
                    }
                }
                if (conflictPredicted) break;
            }

            if (conflictPredicted)
            {
                var result = MessageBox.Show(
                    $"Se predice conflicto entre los vuelos {_flightPlans.GetFlightPlan(conflictA).GetId()} y {_flightPlans.GetFlightPlan(conflictB).GetId()}.\n" +
                    "¿Desea resolver el conflicto automáticamente ajustando la velocidad de uno de los vuelos?",
                    "Conflicto futuro detectado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Intenta resolver el conflicto reduciendo la velocidad de uno de los vuelos.
                    bool resolved = ResolveConflictBySpeed(_flightPlans.GetFlightPlan(conflictA), _flightPlans.GetFlightPlan(conflictB), _securityDistance);
                    if (resolved)
                    {
                        MessageBox.Show(
                            $"La velocidad del vuelo {_flightPlans.GetFlightPlan(conflictB).GetId()} ha sido ajustada para evitar el conflicto.",
                            "Conflicto resuelto",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"No se pudo resolver el conflicto ajustando la velocidad.",
                            "Conflicto no resuelto",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                // Si el usuario responde No, la simulación continúa tal cual.
            }

            simulationTimer.Interval = (int)(_cycleTime * 1000); // Update interval in case _cycleTime changed
            simulationTimer.Start();
        }

        // Reduce progresivamente la velocidad de b hasta que deje de predecirse conflicto.
        private bool ResolveConflictBySpeed(FlightPlan a, FlightPlan b, double securityDistance)
        {
            double originalSpeed = b.GetVelocidad();
            double minSpeed = 0.1; // Minimum allowed speed (avoid zero)
            double step = originalSpeed / 20.0; // Try 20 steps

            for (double newSpeed = originalSpeed - step; newSpeed >= minSpeed; newSpeed -= step)
            {
                b.SetVelocidad(newSpeed);
                if (!WillFlightsConflict(a, b, securityDistance))
                {
                    return true; // Conflict resolved
                }
            }
            b.SetVelocidad(originalSpeed); // Restore if not resolved
            return false;
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            simulationTimer.Stop();
        }

        private void infobtn_Click(object sender, EventArgs e)
        {
            // Abre una vista tabular para inspeccionar distancias y editar velocidades.
            using (FlightGrid form = new FlightGrid(_flightPlans))
            {
                if (form.ShowDialog(this) == DialogResult.OK && form.SpeedsUpdated)
                {
                    // Requisito v1: al cambiar velocidades se reinicia la simulación
                    // en posiciones iniciales para volver a verificar conflicto.
                    RestartSimulationAfterSpeedChange();
                }
            }
        }

        // Revisa si algún par de vuelos está actualmente por debajo de la distancia de seguridad.
        private void CheckConflicts()
        {
            int numFlights = _flightPlans.getnum();
            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                for (int j = i + 1; j < numFlights; j++)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);
                    if (a.Conflicto(b, _securityDistance))
                    {
                        MessageBox.Show(
                            $"¡Conflicto detectado entre los vuelos {a.GetId()} y {b.GetId()}!\n" +
                            $"Distancia menor a la de seguridad ({_securityDistance}).",
                            "Conflicto de vuelos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return; // Solo muestra el primer conflicto encontrado en este ciclo
                    }
                }
            }
        }

        private void conflictbtn_Click(object sender, EventArgs e)
        {
            // Permite consultar manualmente si habrá conflicto en el futuro.
            int numFlights = _flightPlans.getnum();
            bool conflictPredicted = false;
            string message = "";

            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                for (int j = i + 1; j < numFlights; j++)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);

                    // Predict if their paths will ever be closer than _securityDistance
                    if (WillFlightsConflict(a, b, _securityDistance))
                    {
                        message += $"Se predice conflicto entre los vuelos {a.GetId()} y {b.GetId()}.\n";
                        conflictPredicted = true;
                    }
                }
            }

            if (conflictPredicted)
            {
                MessageBox.Show(message, "Conflicto futuro detectado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("No se predicen conflictos futuros entre los vuelos.", "Sin conflicto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private bool WillFlightsConflict(FlightPlan a, FlightPlan b, double securityDistance)
        {
            Position aStart = a.GetInitialPosition();
            Position aEnd = a.GetFinalPosition();
            Position bStart = b.GetInitialPosition();
            Position bEnd = b.GetFinalPosition();

            // Normaliza la dirección de cada ruta para calcular velocidades coherentes.
            double ax = aEnd.GetX() - aStart.GetX();
            double ay = aEnd.GetY() - aStart.GetY();
            double bx = bEnd.GetX() - bStart.GetX();
            double by = bEnd.GetY() - bStart.GetY();
            Normalize(ref ax, ref ay);
            Normalize(ref bx, ref by);

            // Velocidad relativa de ambos vuelos.
            double vax = ax * a.GetVelocidad();
            double vay = ay * a.GetVelocidad();
            double vbx = bx * b.GetVelocidad();
            double vby = by * b.GetVelocidad();

            // Posición y velocidad relativas.
            double rx = aStart.GetX() - bStart.GetX();
            double ry = aStart.GetY() - bStart.GetY();
            double vx = vax - vbx;
            double vy = vay - vby;

            // Busca el instante futuro en el que la distancia sería mínima.
            double tMin = 0;
            double denom = vx * vx + vy * vy;
            if (denom != 0)
            {
                tMin = -(rx * vx + ry * vy) / denom;
                tMin = Math.Max(0, tMin); // Only future times
            }

            // Calcula las posiciones estimadas en ese instante.
            double aX = aStart.GetX() + ax * a.GetVelocidad() * tMin;
            double aY = aStart.GetY() + ay * a.GetVelocidad() * tMin;
            double bX = bStart.GetX() + bx * b.GetVelocidad() * tMin;
            double bY = bStart.GetY() + by * b.GetVelocidad() * tMin;

            double dist = Math.Sqrt((aX - bX) * (aX - bX) + (aY - bY) * (aY - bY));
            return dist < securityDistance;
        }

        private static void Normalize(ref double x, ref double y)
        {
            double length = Math.Sqrt(x * x + y * y);
            if (length == 0)
            {
                x = 0;
                y = 0;
                return;
            }

            x /= length;
            y /= length;
        }

        private static Bitmap CreateFlightMarkerImage()
        {
            Bitmap bitmap = new Bitmap(20, 20);
            using (Graphics g = Graphics.FromImage(bitmap))
            using (SolidBrush fillBrush = new SolidBrush(Color.DarkRed))
            using (Pen borderPen = new Pen(Color.White, 2))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                g.FillEllipse(fillBrush, 2, 2, 16, 16);
                g.DrawEllipse(borderPen, 2, 2, 16, 16);
            }

            return bitmap;
        }

        private void MoveFlightsOneCycle()
        {
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Mover(_cycleTime);
            }

            UpdateVisualPositionsFromCurrentState();
            // Comprobar conflictos después de mover los vuelos.
            CheckConflicts();
        }

        private void UpdateVisualPositionsFromCurrentState()
        {
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                Position currentPosition = _flightPlans.GetFlightPlan(i).GetCurrentPosition();
                int x = (int)currentPosition.GetX() - flights[i].Width / 2;
                int y = panel1.Height - (int)currentPosition.GetY() - flights[i].Height / 2;
                flights[i].Location = new Point(x, y);
            }

            panel1.Invalidate();
        }

        private void RestartSimulationAfterSpeedChange()
        {
            simulationTimer.Stop();

            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                _flightPlans.GetFlightPlan(i).Restart();
            }

            UpdateVisualPositionsFromCurrentState();

            if (TryBuildFutureConflictMessage(out string message))
            {
                MessageBox.Show(
                    "Se han actualizado las velocidades y se ha reiniciado la simulación.\n" + message,
                    "Velocidades aplicadas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se han actualizado las velocidades y se ha reiniciado la simulación.\nNo se predicen conflictos futuros.",
                    "Velocidades aplicadas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private bool TryBuildFutureConflictMessage(out string message)
        {
            int numFlights = _flightPlans.getnum();
            StringBuilder builder = new StringBuilder();
            bool predicted = false;

            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                for (int j = i + 1; j < numFlights; j++)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);
                    if (WillFlightsConflict(a, b, _securityDistance))
                    {
                        builder.AppendLine($"Se predice conflicto entre los vuelos {a.GetId()} y {b.GetId()}.");
                        predicted = true;
                    }
                }
            }

            message = builder.ToString().Trim();
            return predicted;
        }
    }
}

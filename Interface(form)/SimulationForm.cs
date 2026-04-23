using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
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
        private readonly Timer simulationTimer;

        private const int FlightIdColumnIndex = 0;
        private const int FlightPositionColumnIndex = 1;
        private const int FlightSpeedColumnIndex = 2;

        private PictureBox[] flights;
        private bool _updatingGrid;
        private bool _resumeAfterGridEdit;

        public SimulationForm(FlightPlanList flightPlans, double cycleTime, double securityDistance)
        {
            InitializeComponent();
            _flightPlans = flightPlans;
            _cycleTime = cycleTime;
            _securityDistance = securityDistance;
            panel1.Paint += panel1_Paint;

            ConfigureFlightListGrid();

            // Temporizador para ejecutar automáticamente los mismos pasos del botón Cycle.
            simulationTimer = new Timer();
            simulationTimer.Interval = (int)(_cycleTime * 1000); // Convert seconds to milliseconds
            simulationTimer.Tick += timer1_Tick;
        }

        private void ConfigureFlightListGrid()
        {
            flightListGrid.AutoGenerateColumns = false;
            flightListGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            flightListGrid.MultiSelect = false;
            flightListGrid.RowHeadersVisible = false;
            flightListGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;

            flightListGrid.Columns.Clear();
            flightListGrid.Columns.Add(CreateTextColumn("FlightId", "ID", true));
            flightListGrid.Columns.Add(CreateTextColumn("FlightPosition", "Posicion actual", true));
            flightListGrid.Columns.Add(CreateTextColumn("FlightSpeed", "Velocidad", false));

            flightListGrid.CellBeginEdit += flightListGrid_CellBeginEdit;
            flightListGrid.CellValidating += flightListGrid_CellValidating;
            flightListGrid.CellEndEdit += flightListGrid_CellEndEdit;
            flightListGrid.DataError += flightListGrid_DataError;
        }

        private static DataGridViewTextBoxColumn CreateTextColumn(string name, string headerText, bool readOnly)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                ReadOnly = readOnly,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
        }

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            // Crea un icono por vuelo y lo sitúa según la posición actual del plan.
            flights = new PictureBox[_flightPlans.getnum()];
            int i = 0;
            while (i < _flightPlans.getnum())
            {
                PictureBox p = new PictureBox();
                FlightPlan f = _flightPlans.GetFlightPlan(i);

                p.Width = 20;
                p.Height = 20;
                p.ClientSize = new Size(20, 20);
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                p.Image = CreateFlightMarkerImage();

                p.Tag = i;
                p.Click += new EventHandler(this.flightInfo);

                panel1.Controls.Add(p);
                flights[i] = p;
                i++;
            }

            // Envía los iconos al fondo para que las trayectorias y elipses se dibujen encima.
            foreach (Control c in panel1.Controls)
            {
                c.SendToBack();
            }

            RefreshFlightMarkers();
            RefreshFlightList();
            panel1.Invalidate();
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
            AdvanceOneStep();
        }

        private void AdvanceOneStep()
        {
            // Ejecuta un único ciclo: mover, refrescar pantalla y revisar conflictos.
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Mover(_cycleTime);
            }

            RefreshFlightMarkers();
            RefreshFlightList();
            panel1.Invalidate();
            CheckConflicts();
        }

        private void RefreshFlightMarkers()
        {
            if (flights == null)
            {
                return;
            }

            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                Position currentPosition = flight.GetCurrentPosition();
                int x = (int)currentPosition.GetX() - flights[i].Width / 2;
                int y = panel1.Height - (int)currentPosition.GetY() - flights[i].Height / 2;

                flights[i].Location = new Point(x, y);
            }
        }

        private void RefreshFlightList()
        {
            _updatingGrid = true;
            try
            {
                flightListGrid.Rows.Clear();

                for (int i = 0; i < _flightPlans.getnum(); i++)
                {
                    FlightPlan plan = _flightPlans.GetFlightPlan(i);
                    Position position = plan.GetCurrentPosition();
                    string positionText = $"({position.GetX():F2}, {position.GetY():F2})";
                    string speedText = plan.GetVelocidad().ToString("F2", CultureInfo.CurrentCulture);

                    flightListGrid.Rows.Add(plan.GetId(), positionText, speedText);
                }
            }
            finally
            {
                _updatingGrid = false;
            }
        }

        private void RestartSimulation(bool resumeAfterRestart)
        {
            bool wasRunning = simulationTimer.Enabled;
            simulationTimer.Stop();

            _flightPlans.RestartAll();
            RefreshFlightMarkers();
            RefreshFlightList();
            panel1.Invalidate();

            if (resumeAfterRestart)
            {
                simulationTimer.Interval = (int)(_cycleTime * 1000);
                simulationTimer.Start();
            }
            else if (wasRunning)
            {
                simulationTimer.Interval = (int)(_cycleTime * 1000);
            }
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
            AdvanceOneStep();
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
                    RefreshFlightList();

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

            simulationTimer.Interval = (int)(_cycleTime * 1000);
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
            // Abre una vista tabular con la información y las distancias entre vuelos.
            FlightGrid form = new FlightGrid(_flightPlans);
            form.ShowDialog(this);
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

        private void flightListGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (_updatingGrid || e.RowIndex < 0 || e.ColumnIndex != FlightSpeedColumnIndex)
            {
                return;
            }

            _resumeAfterGridEdit = simulationTimer.Enabled;
            simulationTimer.Stop();
        }

        private void flightListGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_updatingGrid || e.RowIndex < 0 || e.ColumnIndex != FlightSpeedColumnIndex)
            {
                return;
            }

            string valueText = Convert.ToString(e.FormattedValue)?.Trim();
            if (!TryParsePositiveDouble(valueText, out _))
            {
                e.Cancel = true;
                MessageBox.Show(
                    "La velocidad debe ser un valor numérico mayor que 0.",
                    "Error de validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void flightListGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_updatingGrid || e.RowIndex < 0 || e.ColumnIndex != FlightSpeedColumnIndex)
            {
                return;
            }

            try
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(e.RowIndex);
                if (flight == null)
                {
                    ResumeTimerAfterGridEditIfNeeded();
                    return;
                }

                string valueText = Convert.ToString(flightListGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)?.Trim();
                if (!TryParsePositiveDouble(valueText, out double newSpeed))
                {
                    RefreshFlightList();
                    ResumeTimerAfterGridEditIfNeeded();
                    return;
                }

                if (Math.Abs(flight.GetVelocidad() - newSpeed) < 1e-6)
                {
                    RefreshFlightList();
                    ResumeTimerAfterGridEditIfNeeded();
                    return;
                }

                flight.SetVelocidad(newSpeed);
                RestartSimulation(true);
            }
            finally
            {
                _resumeAfterGridEdit = false;
            }
        }

        private void flightListGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void ResumeTimerAfterGridEditIfNeeded()
        {
            if (_resumeAfterGridEdit)
            {
                simulationTimer.Interval = (int)(_cycleTime * 1000);
                simulationTimer.Start();
            }
        }

        private static bool TryParsePositiveDouble(string text, out double value)
        {
            return (double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value) ||
                    double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                   && value > 0;
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
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    public partial class SimulationForm : Form
    {
        // ── Datos compartidos de la simulación ───────────────────────────────────

        private readonly FlightPlanList _flightPlans;
        private readonly double _cycleTime;
        private readonly double _securityDistance;

        // Iconos visuales de cada avión sobre el panel.
        PictureBox[] flights;

        // Temporizador para la simulación automática.
        private Timer simulationTimer;

        // Evita que la edición de la tabla dispare eventos anidados.
        private bool _updatingGrid = false;

        // ── Constructor ──────────────────────────────────────────────────────────

        public SimulationForm(FlightPlanList flightPlans, double cycleTime, double securityDistance)
        {
            InitializeComponent();
            _flightPlans      = flightPlans;
            _cycleTime        = cycleTime;
            _securityDistance = securityDistance;

            // Vincular el evento de pintado personalizado del panel.
            panel1.Paint += panel1_Paint;

            // Configurar el temporizador para la simulación automática.
            simulationTimer          = new Timer();
            simulationTimer.Interval = (int)(_cycleTime * 1000); // segundos → milisegundos
            simulationTimer.Tick    += timer1_Tick;
        }

        // ── Carga del formulario ─────────────────────────────────────────────────

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            // Crear un icono PictureBox por cada vuelo y situarlo en su posición inicial.
            flights = new PictureBox[_flightPlans.getnum()];
            int i = 0;
            while (i < _flightPlans.getnum())
            {
                PictureBox p   = new PictureBox();
                FlightPlan f   = _flightPlans.GetFlightPlan(i);

                p.Width      = 30;
                p.Height     = 30;
                p.ClientSize = new Size(30, 30);

                // Transparente para que el fondo del panel sea visible alrededor del círculo.
                p.BackColor = System.Drawing.Color.Transparent;

                Position initialPosition = f.GetInitialPosition();
                int x = (int)initialPosition.GetX() - p.Width / 2;
                int y = panel1.Height - (int)initialPosition.GetY() - p.Height / 2;

                p.Location  = new Point(x, y);
                p.SizeMode  = PictureBoxSizeMode.StretchImage;
                p.Image     = CreateFlightMarkerImage();
                p.Tag       = i;
                p.Click    += new System.EventHandler(this.flightInfo);

                panel1.Controls.Add(p);
                flights[i] = p;
                i++;
            }

            // Enviar todos los controles del panel al fondo para que las líneas se dibujen encima.
            foreach (Control c in panel1.Controls)
                c.SendToBack();

            // Cargar la tabla de velocidades con los datos iniciales.
            LoadSpeedGrid();
        }

        // ── Clic en un icono de vuelo ────────────────────────────────────────────

        private void flightInfo(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            int i = (int)p.Tag;
            FlightInfo f = new FlightInfo();
            f.setFlight(_flightPlans.GetFlightPlan(i));
            f.ShowDialog();
        }

        // ── Paso manual (Cycle) ──────────────────────────────────────────────────

        private void cyclebtn_Click(object sender, EventArgs e)
        {
            // Guardar snapshot ANTES de mover para permitir Undo.
            _flightPlans.SaveSnapshot();

            // Mover todos los vuelos un ciclo y actualizar sus iconos.
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Mover(_cycleTime);

                Position currentPosition = flight.GetCurrentPosition();
                int x = (int)currentPosition.GetX() - flights[i].Width / 2;
                int y = panel1.Height - (int)currentPosition.GetY() - flights[i].Height / 2;

                flights[i].Location = new Point(x, y);
            }
            panel1.Invalidate();

            // Revisar conflictos después del movimiento.
            CheckConflicts();
        }

        // ── Pintado del panel ────────────────────────────────────────────────────

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Black, 2))
            using (Pen circlePen = new Pen(Color.Blue, 2))
            {
                pen.DashStyle = DashStyle.Dash;
                for (int i = 0; i < _flightPlans.getnum(); i++)
                {
                    FlightPlan flight = _flightPlans.GetFlightPlan(i);
                    Position origin   = flight.GetInitialPosition();
                    Position dest     = flight.GetFinalPosition();

                    int x1 = (int)origin.GetX();
                    int y1 = panel1.Height - (int)origin.GetY();
                    int x2 = (int)dest.GetX();
                    int y2 = panel1.Height - (int)dest.GetY();

                    // Trazar la trayectoria como línea discontinua.
                    e.Graphics.DrawLine(pen, x1, y1, x2, y2);

                    Position current = flight.GetCurrentPosition();
                    float centerX = (float)current.GetX();
                    float centerY = panel1.Height - (float)current.GetY();
                    float radius  = (float)_securityDistance;

                    // Dibujar el círculo de distancia de seguridad.
                    e.Graphics.DrawEllipse(circlePen,
                        centerX - radius, centerY - radius,
                        radius * 2,       radius * 2);
                }
            }
        }

        // ── Tick del temporizador automático ────────────────────────────────────

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Guardar snapshot ANTES de mover para permitir Undo.
            _flightPlans.SaveSnapshot();

            // Mismo avance que el botón Cycle, ejecutado automáticamente.
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Mover(_cycleTime);

                Position currentPosition = flight.GetCurrentPosition();
                int x = (int)currentPosition.GetX() - flights[i].Width / 2;
                int y = panel1.Height - (int)currentPosition.GetY() - flights[i].Height / 2;

                flights[i].Location = new Point(x, y);
            }
            panel1.Invalidate();

            CheckConflicts();
        }

        // ── Botón Start (simulación automática) ─────────────────────────────────

        private void startbtn_Click(object sender, EventArgs e)
        {
            // Antes de iniciar la simulación automática, comprobar conflictos previstos.
            int numFlights       = _flightPlans.getnum();
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
                DialogResult result = MessageBox.Show(
                    "Se predice conflicto entre los vuelos " +
                    _flightPlans.GetFlightPlan(conflictA).GetId() + " y " +
                    _flightPlans.GetFlightPlan(conflictB).GetId() + ".\n" +
                    "¿Desea resolver el conflicto automáticamente ajustando la velocidad?",
                    "Conflicto futuro detectado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool resolved = ResolveConflictBySpeed(
                        _flightPlans.GetFlightPlan(conflictA),
                        _flightPlans.GetFlightPlan(conflictB),
                        _securityDistance);

                    if (resolved)
                    {
                        MessageBox.Show(
                            "La velocidad del vuelo " +
                            _flightPlans.GetFlightPlan(conflictB).GetId() +
                            " ha sido ajustada para evitar el conflicto.",
                            "Conflicto resuelto",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Actualizar la tabla de velocidades tras el ajuste automático.
                        LoadSpeedGrid();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo resolver el conflicto ajustando la velocidad.",
                            "Conflicto no resuelto",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }

            simulationTimer.Interval = (int)(_cycleTime * 1000);
            simulationTimer.Start();
        }

        // ── Botón Stop ───────────────────────────────────────────────────────────

        private void stopbtn_Click(object sender, EventArgs e)
        {
            simulationTimer.Stop();
        }

        // ── Botón Space Info ─────────────────────────────────────────────────────

        private void infobtn_Click(object sender, EventArgs e)
        {
            FlightGrid form = new FlightGrid(_flightPlans);
            form.ShowDialog(this);
        }

        // ── Botón Restart ────────────────────────────────────────────────────────

        private void restartbtn_Click(object sender, EventArgs e)
        {
            RestartSimulation();
        }

        // ── Botón Check Conflict ─────────────────────────────────────────────────

        private void conflictbtn_Click(object sender, EventArgs e)
        {
            int numFlights     = _flightPlans.getnum();
            bool conflictPredicted = false;
            string message     = string.Empty;

            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                for (int j = i + 1; j < numFlights; j++)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);
                    if (WillFlightsConflict(a, b, _securityDistance))
                    {
                        message += "Se predice conflicto entre los vuelos " +
                                   a.GetId() + " y " + b.GetId() + ".\n";
                        conflictPredicted = true;
                    }
                }
            }

            if (conflictPredicted)
            {
                MessageBox.Show(message, "Conflicto futuro detectado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("No se predicen conflictos futuros.",
                    "Sin conflicto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ── Botón Undo Step (nuevo en v2) ────────────────────────────────────────

        private void undobtn_Click(object sender, EventArgs e)
        {
            // Intentar restaurar las posiciones del paso anterior.
            bool undone = _flightPlans.UndoLastStep();

            if (undone)
            {
                // Actualizar la posición visual de cada icono de vuelo.
                for (int i = 0; i < _flightPlans.getnum(); i++)
                {
                    Position pos = _flightPlans.GetFlightPlan(i).GetCurrentPosition();
                    int x = (int)pos.GetX() - flights[i].Width  / 2;
                    int y = panel1.Height - (int)pos.GetY() - flights[i].Height / 2;
                    flights[i].Location = new Point(x, y);
                }
                panel1.Invalidate();
            }
            else
            {
                MessageBox.Show(
                    "No hay paso anterior para deshacer.",
                    "Undo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        // ── Botón Save State (nuevo en v2) ───────────────────────────────────────

        private void savebtn_Click(object sender, EventArgs e)
        {
            // Abrir diálogo para elegir dónde guardar el archivo.
            SaveFileDialog dlg  = new SaveFileDialog();
            dlg.Title           = "Guardar estado de la simulación";
            dlg.Filter          = "Archivos de sesión|*.txt|Todos|*.*";
            dlg.DefaultExt      = "txt";
            dlg.FileName        = "sesion_atc";

            if (dlg.ShowDialog(this) != DialogResult.OK)
            {
                return; // El usuario canceló.
            }

            string path   = dlg.FileName;
            string error  = _flightPlans.SaveToFile(path);

            if (error == null)
            {
                MessageBox.Show(
                    "Estado guardado correctamente en:\n" + path,
                    "Guardado correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Error al guardar el estado:\n" + error,
                    "Error al guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ── Métodos de apoyo ─────────────────────────────────────────────────────

        /// <summary>
        /// Carga (o recarga) la tabla con los IDs y velocidades actuales de cada vuelo.
        /// </summary>
        private void LoadSpeedGrid()
        {
            _updatingGrid = true;
            speedGrid.Rows.Clear();
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan f = _flightPlans.GetFlightPlan(i);
                speedGrid.Rows.Add(f.GetId(), f.GetVelocidad().ToString("F1"));
            }
            _updatingGrid = false;
        }

        /// <summary>
        /// Valida el valor editado en la tabla, lo aplica al vuelo y reinicia la simulación.
        /// </summary>
        private void speedGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_updatingGrid || e.ColumnIndex != 1 || e.RowIndex < 0) return;

            // Aceptar tanto ',' como '.' como separador decimal.
            string rawValue = string.Empty;
            if (speedGrid.Rows[e.RowIndex].Cells[1].Value != null)
            {
                rawValue = speedGrid.Rows[e.RowIndex].Cells[1].Value.ToString().Replace(',', '.');
            }

            double newSpeed;
            bool parsed = double.TryParse(rawValue,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out newSpeed);

            if (!parsed || newSpeed <= 0)
            {
                MessageBox.Show("Introduce una velocidad positiva válida.", "Velocidad inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _updatingGrid = true;
                speedGrid.Rows[e.RowIndex].Cells[1].Value =
                    _flightPlans.GetFlightPlan(e.RowIndex).GetVelocidad().ToString("F1");
                _updatingGrid = false;
                return;
            }

            _flightPlans.GetFlightPlan(e.RowIndex).SetVelocidad(newSpeed);
            RestartSimulation();
        }

        /// <summary>
        /// Detiene el temporizador, resetea todos los vuelos a sus posiciones iniciales y redibuja.
        /// </summary>
        private void RestartSimulation()
        {
            simulationTimer.Stop();
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Restart();
                Position ini = flight.GetInitialPosition();
                int x = (int)ini.GetX() - (flights[i].Width  / 2);
                int y = panel1.Height - (int)ini.GetY() - (flights[i].Height / 2);
                flights[i].Location = new Point(x, y);
            }
            panel1.Invalidate();
            LoadSpeedGrid();
        }

        /// <summary>
        /// Revisa si algún par de vuelos está por debajo de la distancia de seguridad.
        /// </summary>
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
                            "¡Conflicto detectado entre los vuelos " + a.GetId() +
                            " y " + b.GetId() + "!\n" +
                            "Distancia menor a la de seguridad (" + _securityDistance + ").",
                            "Conflicto de vuelos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return; // Solo mostrar el primer conflicto del ciclo
                    }
                }
            }
        }

        /// <summary>
        /// Predice si dos vuelos llegarán a estar más cerca que la distancia de seguridad.
        /// </summary>
        private bool WillFlightsConflict(FlightPlan a, FlightPlan b, double securityDistance)
        {
            // Usar posición ACTUAL para predecir desde el momento presente,
            // no desde el origen — así Check Conflict funciona también a mitad de simulación.
            Position aStart = a.GetCurrentPosition();
            Position aEnd   = a.GetFinalPosition();
            Position bStart = b.GetCurrentPosition();
            Position bEnd   = b.GetFinalPosition();

            double ax = aEnd.GetX() - aStart.GetX();
            double ay = aEnd.GetY() - aStart.GetY();
            double bx = bEnd.GetX() - bStart.GetX();
            double by = bEnd.GetY() - bStart.GetY();
            Normalize(ref ax, ref ay);
            Normalize(ref bx, ref by);

            double vax = ax * a.GetVelocidad();
            double vay = ay * a.GetVelocidad();
            double vbx = bx * b.GetVelocidad();
            double vby = by * b.GetVelocidad();

            double rx = aStart.GetX() - bStart.GetX();
            double ry = aStart.GetY() - bStart.GetY();
            double vx = vax - vbx;
            double vy = vay - vby;

            double tMin  = 0;
            double denom = vx * vx + vy * vy;
            if (denom != 0)
            {
                tMin = -(rx * vx + ry * vy) / denom;
                tMin = Math.Max(0, tMin);
            }

            double aX   = aStart.GetX() + ax * a.GetVelocidad() * tMin;
            double aY   = aStart.GetY() + ay * a.GetVelocidad() * tMin;
            double bX   = bStart.GetX() + bx * b.GetVelocidad() * tMin;
            double bY   = bStart.GetY() + by * b.GetVelocidad() * tMin;

            double dist = Math.Sqrt((aX - bX) * (aX - bX) + (aY - bY) * (aY - bY));
            return dist < securityDistance;
        }

        /// <summary>
        /// Reduce progresivamente la velocidad del vuelo b hasta eliminar el conflicto predicho.
        /// </summary>
        private bool ResolveConflictBySpeed(FlightPlan a, FlightPlan b, double securityDistance)
        {
            double originalSpeed = b.GetVelocidad();
            double minSpeed      = 0.1;
            double step          = originalSpeed / 20.0;

            double newSpeed = originalSpeed - step;
            while (newSpeed >= minSpeed)
            {
                b.SetVelocidad(newSpeed);
                if (!WillFlightsConflict(a, b, securityDistance))
                {
                    return true; // Conflicto resuelto
                }
                newSpeed -= step;
            }

            b.SetVelocidad(originalSpeed); // Restaurar si no se pudo resolver
            return false;
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
            Bitmap bitmap = new Bitmap(30, 30);
            using (Graphics g = Graphics.FromImage(bitmap))
            using (SolidBrush fillBrush = new SolidBrush(Color.Red))
            using (Pen borderPen = new Pen(Color.Black, 2))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                g.FillEllipse(fillBrush, 2, 2, 26, 26);
                g.DrawEllipse(borderPen, 2, 2, 26, 26);
            }
            return bitmap;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FlightLib
{
    /// <summary>
    /// Contenedor de planes de vuelo activos.
    /// En v2 usa List&lt;FlightPlan&gt; (sin límite fijo), añade snapshot/undo
    /// y persistencia en fichero CSV separado por punto y coma.
    /// </summary>
    public class FlightPlanList
    {
        // ── Colección principal ──────────────────────────────────────────────────

        // Lista dinámica de vuelos — permite cualquier número de planes (req. v2).
        private List<FlightPlan> _plans = new List<FlightPlan>();

        // ── Snapshot para Undo ───────────────────────────────────────────────────

        // Copia de las posiciones antes del último paso de simulación.
        private List<Position> _snapshot = new List<Position>();

        // Indica si existe una instantánea válida que se pueda restaurar.
        private bool _hasSnapshot = false;

        // ── API pública (misma firma que v1) ─────────────────────────────────────

        /// <summary>Devuelve el número de vuelos actualmente cargados.</summary>
        public int getnum()
        {
            return _plans.Count;
        }

        /// <summary>
        /// Añade un vuelo a la lista.
        /// En v2 no hay límite, por lo que siempre devuelve 0 (éxito).
        /// </summary>
        public int AddFlightPlan(FlightPlan p)
        {
            _plans.Add(p);
            return 0; // siempre éxito en v2
        }

        /// <summary>
        /// Devuelve el vuelo en la posición indicada, o null si el índice no es válido.
        /// </summary>
        public FlightPlan GetFlightPlan(int i)
        {
            // Validar rango para no lanzar excepción.
            if (i < 0 || i >= _plans.Count)
            {
                return null;
            }
            return _plans[i];
        }

        /// <summary>Avanza todos los vuelos el mismo intervalo de tiempo.</summary>
        public void Mover(double tiempo)
        {
            int i = 0;
            while (i < _plans.Count)
            {
                _plans[i].Mover(tiempo);
                i++;
            }
        }

        /// <summary>Vacía la colección de vuelos y descarta cualquier snapshot.</summary>
        public void Clear()
        {
            _plans.Clear();
            _snapshot.Clear();
            _hasSnapshot = false;
        }

        /// <summary>Muestra por consola el estado de todos los vuelos cargados.</summary>
        public void EscribeConsola()
        {
            int i = 0;
            while (i < _plans.Count)
            {
                _plans[i].EscribeConsola();
                i++;
            }
        }

        // ── Snapshot / Undo ──────────────────────────────────────────────────────

        /// <summary>
        /// Guarda una copia de las posiciones actuales de todos los vuelos.
        /// Debe llamarse ANTES del paso de simulación que se quiera poder deshacer.
        /// </summary>
        public void SaveSnapshot()
        {
            _snapshot.Clear();

            // Copiar la posición actual de cada vuelo como nueva instancia de Position.
            for (int i = 0; i < _plans.Count; i++)
            {
                Position pos = _plans[i].GetCurrentPosition();
                _snapshot.Add(new Position(pos.GetX(), pos.GetY()));
            }

            _hasSnapshot = true;
        }

        /// <summary>
        /// Restaura las posiciones guardadas en el último snapshot.
        /// </summary>
        /// <returns>
        /// true  si se restauró correctamente;
        /// false si no había snapshot disponible.
        /// </returns>
        public bool UndoLastStep()
        {
            // Sin snapshot no hay nada que restaurar.
            if (!_hasSnapshot)
            {
                return false;
            }

            // El número de entradas en el snapshot debe coincidir con la lista actual.
            if (_snapshot.Count != _plans.Count)
            {
                // Inconsistencia — descartar snapshot y reportar fallo.
                _hasSnapshot = false;
                _snapshot.Clear();
                return false;
            }

            // Restaurar cada vuelo a su posición guardada.
            for (int i = 0; i < _plans.Count; i++)
            {
                Position saved = _snapshot[i];
                _plans[i].SetCurrentPosition(new Position(saved.GetX(), saved.GetY()));
            }

            // Consumir el snapshot para que no se pueda aplicar dos veces.
            _hasSnapshot = false;
            _snapshot.Clear();
            return true;
        }

        // ── Persistencia en archivo ──────────────────────────────────────────────

        /// <summary>
        /// Carga vuelos desde un archivo CSV separado por punto y coma.
        /// Formatos aceptados por línea:
        ///   7 campos: ID;Company;OriginX;OriginY;DestX;DestY;Speed
        ///   9 campos: ID;Company;OriginX;OriginY;DestX;DestY;Speed;CurrentX;CurrentY
        /// Las líneas que empiecen por '#' o que contengan "ID;" se ignoran (cabecera/comentario).
        /// </summary>
        /// <param name="filePath">Ruta al archivo a leer.</param>
        /// <returns>null si todo fue bien; mensaje de error en caso contrario.</returns>
        public string LoadFromFile(string filePath)
        {
            // Validar parámetro de entrada.
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return "La ruta del archivo no puede estar vacía.";
            }

            try
            {
                // Leer todas las líneas del archivo de una vez.
                string[] lines = File.ReadAllLines(filePath);

                // Limpiar la lista actual antes de cargar nuevos datos.
                Clear();

                int lineNumber = 0;
                int loaded = 0;

                for (int i = 0; i < lines.Length; i++)
                {
                    lineNumber++;
                    string line = lines[i].Trim();

                    // Ignorar líneas vacías, comentarios y cabecera.
                    if (string.IsNullOrEmpty(line))        { continue; }
                    if (line.StartsWith("#"))              { continue; }
                    if (line.IndexOf("ID;") >= 0)         { continue; }

                    // Separar los campos por punto y coma.
                    string[] fields = line.Split(';');

                    if (fields.Length != 7 && fields.Length != 9)
                    {
                        return "Formato incorrecto en línea " + lineNumber +
                               ": se esperan 7 o 9 campos separados por ';'.";
                    }

                    string id      = fields[0].Trim();
                    string company = fields[1].Trim();

                    // Parsear coordenadas y velocidad con cultura invariante
                    // para que el separador decimal siempre sea el punto.
                    double originX, originY, destX, destY, speed;

                    bool ok =
                        double.TryParse(fields[2].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out originX) &&
                        double.TryParse(fields[3].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out originY) &&
                        double.TryParse(fields[4].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out destX)   &&
                        double.TryParse(fields[5].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out destY)   &&
                        double.TryParse(fields[6].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out speed);

                    if (!ok)
                    {
                        return "No se pudieron parsear los valores numéricos en línea " + lineNumber + ".";
                    }

                    // Construir el FlightPlan con origen y destino.
                    FlightPlan fp = new FlightPlan(id, company, originX, originY, destX, destY, speed);

                    // Si hay 9 campos, restaurar la posición actual guardada en la sesión.
                    if (fields.Length == 9)
                    {
                        double curX, curY;
                        bool okCurrent =
                            double.TryParse(fields[7].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out curX) &&
                            double.TryParse(fields[8].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out curY);

                        if (!okCurrent)
                        {
                            return "No se pudieron parsear las posiciones actuales en línea " + lineNumber + ".";
                        }

                        // Sobreescribir la posición actual con la guardada.
                        fp.SetCurrentPosition(new Position(curX, curY));
                    }

                    _plans.Add(fp);
                    loaded++;
                }

                // Informar si el archivo estaba completamente vacío de datos válidos.
                if (loaded == 0)
                {
                    return "El archivo no contenía ningún plan de vuelo válido.";
                }

                return null; // null indica éxito
            }
            catch (Exception ex)
            {
                // Capturar cualquier error de I/O u otro imprevisto.
                return "Error al leer el archivo: " + ex.Message;
            }
        }

        /// <summary>
        /// Guarda todos los vuelos actuales en formato CSV de 9 columnas.
        /// Cabecera con fecha/hora y columnas:
        ///   ID;Company;OriginX;OriginY;DestX;DestY;Speed;CurrentX;CurrentY
        /// </summary>
        /// <param name="filePath">Ruta donde se escribirá el archivo.</param>
        /// <returns>null si todo fue bien; mensaje de error en caso contrario.</returns>
        public string SaveToFile(string filePath)
        {
            // Validar parámetro de entrada.
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return "La ruta del archivo no puede estar vacía.";
            }

            try
            {
                // Construir el contenido completo en memoria antes de escribir.
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                // Cabecera informativa con fecha y hora de guardado.
                sb.AppendLine("# ATC Deconflicting Tool v2 - Sesión guardada: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.AppendLine("# ID;Company;OriginX;OriginY;DestX;DestY;Speed;CurrentX;CurrentY");

                for (int i = 0; i < _plans.Count; i++)
                {
                    FlightPlan fp = _plans[i];

                    Position origin  = fp.GetInitialPosition();
                    Position dest    = fp.GetFinalPosition();
                    Position current = fp.GetCurrentPosition();

                    // Usar InvariantCulture para que el punto sea siempre el separador decimal.
                    string line =
                        fp.GetId()                                         + ";" +
                        fp.GetCompany()                                    + ";" +
                        origin.GetX().ToString("F4", CultureInfo.InvariantCulture)  + ";" +
                        origin.GetY().ToString("F4", CultureInfo.InvariantCulture)  + ";" +
                        dest.GetX().ToString("F4", CultureInfo.InvariantCulture)    + ";" +
                        dest.GetY().ToString("F4", CultureInfo.InvariantCulture)    + ";" +
                        fp.GetVelocidad().ToString("F4", CultureInfo.InvariantCulture) + ";" +
                        current.GetX().ToString("F4", CultureInfo.InvariantCulture) + ";" +
                        current.GetY().ToString("F4", CultureInfo.InvariantCulture);

                    sb.AppendLine(line);
                }

                File.WriteAllText(filePath, sb.ToString());
                return null; // null indica éxito
            }
            catch (Exception ex)
            {
                return "Error al guardar el archivo: " + ex.Message;
            }
        }
    }
}

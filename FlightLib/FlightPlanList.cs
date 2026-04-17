using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FlightLib
{
    // ============================================================
    // FlightPlanList — Contenedor dinámico de planes de vuelo.
    //
    // Cambios respecto a v1:
    //   · Usa List<FlightPlan> en lugar de un array fijo (sin límite de vuelos).
    //   · Añade snapshot/undo: guarda las posiciones antes de cada paso
    //     de simulación para poder deshacerlo.
    //   · Añade persistencia: carga y guarda planes en archivos CSV
    //     separados por punto y coma (;).
    // ============================================================
    public class FlightPlanList
    {
        // ── Colección principal ──────────────────────────────────────────────────

        // Lista dinámica que crece automáticamente al añadir vuelos.
        // Sustituye al array de tamaño fijo de v1.
        private List<FlightPlan> _plans = new List<FlightPlan>();

        // ── Snapshot para Undo ───────────────────────────────────────────────────

        // Almacena una copia de las posiciones actuales de todos los vuelos
        // justo antes de ejecutar el último paso de simulación.
        // Permite "deshacer" ese paso restaurando estas posiciones.
        private List<Position> _snapshot = new List<Position>();

        // Indica si existe una instantánea válida que se puede restaurar.
        // Se pone a true tras SaveSnapshot() y a false tras UndoLastStep().
        private bool _hasSnapshot = false;

        // ── API pública (misma firma que v1) ─────────────────────────────────────

        /// <summary>
        /// Devuelve el número de vuelos actualmente cargados en la lista.
        /// </summary>
        public int getnum()
        {
            return _plans.Count;
        }

        /// <summary>
        /// Añade un vuelo a la lista.
        /// En v2 no hay límite de vuelos, por lo que siempre devuelve 0 (éxito).
        /// </summary>
        public int AddFlightPlan(FlightPlan p)
        {
            _plans.Add(p);
            return 0; // 0 = éxito; valor mantenido por compatibilidad con v1
        }

        /// <summary>
        /// Devuelve el vuelo en la posición indicada.
        /// Devuelve null si el índice está fuera de rango (en lugar de lanzar excepción).
        /// </summary>
        public FlightPlan GetFlightPlan(int i)
        {
            // Comprobar que el índice sea válido antes de acceder a la lista.
            if (i < 0 || i >= _plans.Count)
            {
                return null;
            }
            return _plans[i];
        }

        /// <summary>
        /// Avanza todos los vuelos el mismo intervalo de tiempo (en minutos).
        /// Itera con while en lugar de foreach siguiendo la convención del proyecto.
        /// </summary>
        public void Mover(double tiempo)
        {
            int i = 0;
            while (i < _plans.Count)
            {
                _plans[i].Mover(tiempo);
                i++;
            }
        }

        /// <summary>
        /// Vacía la lista de vuelos y descarta cualquier snapshot guardado.
        /// Se llama antes de cargar nuevos vuelos desde archivo.
        /// </summary>
        public void Clear()
        {
            _plans.Clear();
            _snapshot.Clear();
            _hasSnapshot = false;
        }

        /// <summary>
        /// Imprime en consola el estado de todos los vuelos cargados.
        /// Útil durante el desarrollo para comprobar los datos sin abrir la UI.
        /// </summary>
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
        /// DEBE llamarse ANTES del paso de simulación que se quiera poder deshacer.
        ///
        /// Se usa el patrón "memento": se crea un nuevo objeto Position para
        /// cada vuelo de modo que la instantánea no comparte referencias con
        /// los objetos de la lista principal.
        /// </summary>
        public void SaveSnapshot()
        {
            // Descartar cualquier snapshot anterior antes de crear uno nuevo.
            _snapshot.Clear();

            // Recorrer todos los vuelos y copiar su posición actual.
            for (int i = 0; i < _plans.Count; i++)
            {
                Position pos = _plans[i].GetCurrentPosition();

                // Crear un nuevo objeto Position para que la copia sea independiente.
                _snapshot.Add(new Position(pos.GetX(), pos.GetY()));
            }

            _hasSnapshot = true;
        }

        /// <summary>
        /// Restaura las posiciones guardadas en el último snapshot.
        /// Permite deshacer un paso de simulación.
        /// </summary>
        /// <returns>
        /// true  si la restauración fue exitosa;
        /// false si no había snapshot disponible o la lista cambió de tamaño.
        /// </returns>
        public bool UndoLastStep()
        {
            // Si no hay snapshot previo, no hay nada que restaurar.
            if (!_hasSnapshot)
            {
                return false;
            }

            // Verificar que el número de vuelos no haya cambiado desde el snapshot.
            // Esto evita escribir en índices incorrectos si se añadieron/eliminaron vuelos.
            if (_snapshot.Count != _plans.Count)
            {
                _hasSnapshot = false;
                _snapshot.Clear();
                return false;
            }

            // Restaurar la posición de cada vuelo con los valores guardados.
            for (int i = 0; i < _plans.Count; i++)
            {
                Position saved = _snapshot[i];
                // Crear una nueva instancia para no compartir referencia con el snapshot.
                _plans[i].SetCurrentPosition(new Position(saved.GetX(), saved.GetY()));
            }

            // Invalidar el snapshot para que no se pueda aplicar dos veces
            // (un solo "undo" por paso de simulación).
            _hasSnapshot = false;
            _snapshot.Clear();
            return true;
        }

        // ── Persistencia en archivo ──────────────────────────────────────────────

        /// <summary>
        /// Carga vuelos desde un archivo CSV separado por punto y coma (;).
        ///
        /// Formatos aceptados por línea:
        ///   7 campos: ID;Compañía;OrigenX;OrigenY;DestinoX;DestinoY;Velocidad
        ///   9 campos: ID;Compañía;OrigenX;OrigenY;DestinoX;DestinoY;Velocidad;CurrentX;CurrentY
        ///
        /// Las líneas que empiecen por '#' o contengan "ID;" se ignoran (cabecera/comentario).
        /// Se usa CultureInfo.InvariantCulture para que el separador decimal sea siempre '.'.
        /// </summary>
        /// <param name="filePath">Ruta al archivo a leer.</param>
        /// <returns>null si todo fue bien; mensaje de error en caso contrario.</returns>
        public string LoadFromFile(string filePath)
        {
            // Validar que se haya proporcionado una ruta no vacía.
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return "La ruta del archivo no puede estar vacía.";
            }

            try
            {
                // Leer todas las líneas del archivo de una sola vez.
                string[] lines = File.ReadAllLines(filePath);

                // Limpiar la lista actual antes de cargar los nuevos datos
                // para evitar mezclar vuelos de sesiones distintas.
                Clear();

                int lineNumber = 0; // Contador de línea para mensajes de error
                int loaded = 0;     // Número de vuelos cargados correctamente

                for (int i = 0; i < lines.Length; i++)
                {
                    lineNumber++;
                    string line = lines[i].Trim(); // Eliminar espacios y retornos de carro

                    // Ignorar líneas vacías.
                    if (string.IsNullOrEmpty(line))        { continue; }

                    // Ignorar comentarios (líneas que empiezan por '#').
                    if (line.StartsWith("#"))              { continue; }

                    // Ignorar la cabecera de columnas si aparece dentro del archivo.
                    if (line.IndexOf("ID;") >= 0)         { continue; }

                    // Dividir la línea en campos usando ';' como separador.
                    string[] fields = line.Split(';');

                    // Solo se aceptan líneas con exactamente 7 o 9 campos.
                    if (fields.Length != 7 && fields.Length != 9)
                    {
                        return "Formato incorrecto en línea " + lineNumber +
                               ": se esperan 7 o 9 campos separados por ';'.";
                    }

                    // Extraer el identificador y la compañía (siempre texto).
                    string id      = fields[0].Trim();
                    string company = fields[1].Trim();

                    // Parsear los valores numéricos usando cultura invariante
                    // para que el separador decimal sea siempre '.' sin importar
                    // la configuración regional del equipo del usuario.
                    // Se inicializan a 0 para satisfacer la comprobación de asignación
                    // definitiva del compilador (CS0165): si TryParse falla en medio
                    // de la cadena &&, los out posteriores no se ejecutan, pero la
                    // variable 'ok' será false y se devolverá error antes de usarlos.
                    double originX = 0, originY = 0, destX = 0, destY = 0, speed = 0;

                    // TryParse devuelve false si algún campo no es un número válido.
                    // El operador && hace que la expresión sea false en cuanto falle uno.
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

                    // Crear el FlightPlan con los datos de origen y destino leídos.
                    // La posición actual coincide con el origen al crearse.
                    FlightPlan fp = new FlightPlan(id, company, originX, originY, destX, destY, speed);

                    // Si hay 9 campos, el archivo contiene también la posición actual
                    // (sesión guardada a mitad de simulación). Se restaura ese estado.
                    if (fields.Length == 9)
                    {
                        double curX = 0, curY = 0;
                        bool okCurrent =
                            double.TryParse(fields[7].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out curX) &&
                            double.TryParse(fields[8].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out curY);

                        if (!okCurrent)
                        {
                            return "No se pudieron parsear las posiciones actuales en línea " + lineNumber + ".";
                        }

                        // Sobreescribir la posición actual con la posición guardada en la sesión.
                        fp.SetCurrentPosition(new Position(curX, curY));
                    }

                    // Añadir el vuelo a la lista principal.
                    _plans.Add(fp);
                    loaded++;
                }

                // Si el archivo no contenía ningún dato válido, indicarlo.
                if (loaded == 0)
                {
                    return "El archivo no contenía ningún plan de vuelo válido.";
                }

                return null; // null indica éxito (ningún mensaje de error)
            }
            catch (Exception ex)
            {
                // Capturar errores de I/O (archivo no existe, sin permisos, etc.)
                return "Error al leer el archivo: " + ex.Message;
            }
        }

        /// <summary>
        /// Guarda todos los vuelos actuales en un archivo CSV de 9 columnas.
        ///
        /// Formato de cada línea:
        ///   ID;Compañía;OrigenX;OrigenY;DestinoX;DestinoY;Velocidad;CurrentX;CurrentY
        ///
        /// Incluye una cabecera con comentarios y la fecha/hora del guardado.
        /// Los valores decimales siempre usan '.' como separador (InvariantCulture).
        /// </summary>
        /// <param name="filePath">Ruta donde se escribirá el archivo.</param>
        /// <returns>null si todo fue bien; mensaje de error en caso contrario.</returns>
        public string SaveToFile(string filePath)
        {
            // Validar que se haya proporcionado una ruta no vacía.
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return "La ruta del archivo no puede estar vacía.";
            }

            try
            {
                // Construir el contenido completo en memoria antes de escribir a disco.
                // Esto evita crear un archivo corrupto si ocurre un error a mitad.
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                // Cabecera con fecha/hora para identificar cuándo se guardó la sesión.
                sb.AppendLine("# ATC Deconflicting Tool v2 - Sesión guardada: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.AppendLine("# ID;Company;OriginX;OriginY;DestX;DestY;Speed;CurrentX;CurrentY");

                // Escribir una línea por cada vuelo con todos sus datos.
                for (int i = 0; i < _plans.Count; i++)
                {
                    FlightPlan fp = _plans[i];

                    // Obtener las tres posiciones relevantes del vuelo.
                    Position origin  = fp.GetInitialPosition();  // Posición de partida
                    Position dest    = fp.GetFinalPosition();     // Posición de destino
                    Position current = fp.GetCurrentPosition();   // Posición en este momento

                    // Construir la línea CSV con 4 decimales y separador de punto.
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

                // Escribir todo el contenido de golpe en el archivo.
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

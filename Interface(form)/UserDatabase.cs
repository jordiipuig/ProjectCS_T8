using System;
using System.IO;

namespace Interface_form_
{
    /// <summary>
    /// Gestiona la autenticación de usuarios mediante un archivo de texto plano.
    /// Formato del archivo: una línea por usuario, separando nombre y contraseña con '|'.
    /// Ejemplo: piloto1|miPass123
    /// </summary>
    public class UserDatabase
    {
        // Ruta completa al archivo de usuarios.
        private string _filePath;

        /// <summary>
        /// Constructor. Recibe la ruta del archivo y lo crea si no existe.
        /// </summary>
        /// <param name="filePath">Ruta absoluta al archivo users.txt</param>
        public UserDatabase(string filePath)
        {
            _filePath = filePath;

            // Crea el archivo vacío si todavía no existe para evitar errores posteriores.
            try
            {
                if (!File.Exists(_filePath))
                {
                    File.WriteAllText(_filePath, string.Empty);
                }
            }
            catch (Exception ex)
            {
                // No se puede lanzar excepción en el constructor; se registra en consola.
                Console.WriteLine("UserDatabase: no se pudo crear el archivo de usuarios: " + ex.Message);
            }
        }

        // ── Métodos de acceso ────────────────────────────────────────────────────

        /// <summary>
        /// Devuelve la ruta del archivo de usuarios.
        /// </summary>
        public string GetFilePath()
        {
            return _filePath;
        }

        /// <summary>
        /// Cambia la ruta del archivo de usuarios.
        /// </summary>
        public void SetFilePath(string value)
        {
            _filePath = value;
        }

        // ── Operaciones de autenticación ─────────────────────────────────────────

        /// <summary>
        /// Comprueba si el par usuario/contraseña coincide con alguna entrada del archivo.
        /// </summary>
        /// <param name="username">Nombre de usuario introducido.</param>
        /// <param name="password">Contraseña introducida.</param>
        /// <returns>true si la combinación es correcta; false en cualquier otro caso.</returns>
        public bool Authenticate(string username, string password)
        {
            // Parámetros vacíos nunca coinciden.
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            try
            {
                // Leer todas las líneas del archivo de usuarios.
                string[] lines = File.ReadAllLines(_filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();

                    // Ignorar líneas vacías o mal formadas.
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    string[] parts = line.Split('|');
                    if (parts.Length != 2)
                    {
                        continue;
                    }

                    string storedUser = parts[0].Trim();
                    string storedPass = parts[1].Trim();

                    // Comparación exacta (sensible a mayúsculas para la contraseña).
                    if (storedUser.Equals(username, StringComparison.Ordinal) &&
                        storedPass.Equals(password, StringComparison.Ordinal))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserDatabase.Authenticate: error leyendo archivo: " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Registra un nuevo usuario si el nombre no está ya en uso.
        /// </summary>
        /// <param name="username">Nombre de usuario deseado.</param>
        /// <param name="password">Contraseña deseada.</param>
        /// <returns>true si el usuario fue creado; false si ya existía o hubo error.</returns>
        public bool Register(string username, string password)
        {
            // Campos vacíos no se admiten.
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            // No permitir duplicados.
            if (UserExists(username))
            {
                return false;
            }

            try
            {
                // Añadir la nueva línea al final del archivo.
                string newLine = username.Trim() + "|" + password.Trim();
                File.AppendAllText(_filePath, newLine + Environment.NewLine);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserDatabase.Register: error escribiendo archivo: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Comprueba si un nombre de usuario ya existe en el archivo.
        /// </summary>
        /// <param name="username">Nombre de usuario a buscar.</param>
        /// <returns>true si existe; false si no o si hay error.</returns>
        public bool UserExists(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            try
            {
                string[] lines = File.ReadAllLines(_filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();

                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    string[] parts = line.Split('|');
                    if (parts.Length < 1)
                    {
                        continue;
                    }

                    string storedUser = parts[0].Trim();

                    // Comparación insensible a mayúsculas para el nombre de usuario.
                    if (storedUser.Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserDatabase.UserExists: error leyendo archivo: " + ex.Message);
            }

            return false;
        }
    }
}

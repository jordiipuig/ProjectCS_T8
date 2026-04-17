using System;
using System.Windows.Forms;

namespace Interface_form_
{
    /// <summary>
    /// Formulario de inicio de sesión y registro de nuevos usuarios.
    /// Muestra dos secciones: una para autenticarse y otra para crear cuenta.
    /// </summary>
    public partial class LoginForm : Form
    {
        // Base de datos de usuarios (archivo users.txt en el directorio de la aplicación).
        private UserDatabase _db;

        // Nombre del usuario que completó el inicio de sesión correctamente.
        private string _loggedUser;

        /// <summary>
        /// Constructor. Inicializa la base de datos de usuarios.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            // Apunta al archivo users.txt junto al ejecutable de la aplicación.
            string usersFilePath = Application.StartupPath + "\\users.txt";
            _db = new UserDatabase(usersFilePath);
            _loggedUser = string.Empty;
        }

        // ── Métodos de acceso ────────────────────────────────────────────────────

        /// <summary>
        /// Devuelve el nombre del usuario que inició sesión correctamente.
        /// Vacío si aún no se ha autenticado nadie.
        /// </summary>
        public string GetLoggedUser()
        {
            return _loggedUser;
        }

        // ── Manejadores de eventos ───────────────────────────────────────────────

        /// <summary>
        /// Carga inicial del formulario.
        /// </summary>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // No se requiere inicialización adicional en la carga.
        }

        /// <summary>
        /// Botón "Entrar": valida los campos e intenta autenticar al usuario.
        /// Si la autenticación es correcta cierra el formulario con DialogResult.OK.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtLoginUser.Text.Trim();
            string password = txtLoginPass.Text;

            // Validar que los campos no estén vacíos.
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Por favor, introduzca el nombre de usuario y la contraseña.",
                    "Campos vacíos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Intentar autenticación.
            bool ok = _db.Authenticate(username, password);

            if (ok)
            {
                _loggedUser = username;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Usuario o contraseña incorrectos. Compruebe sus datos e inténtelo de nuevo.",
                    "Error de autenticación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Botón "Registrarse": valida los campos y crea una nueva cuenta de usuario.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtRegUser.Text.Trim();
            string password = txtRegPass.Text;

            // Validar que los campos no estén vacíos.
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Por favor, introduzca un nombre de usuario y una contraseña para registrarse.",
                    "Campos vacíos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Intentar registrar el nuevo usuario.
            bool registered = _db.Register(username, password);

            if (registered)
            {
                MessageBox.Show(
                    "Usuario \"" + username + "\" registrado correctamente.\nYa puede iniciar sesión.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Limpiar los campos de registro para facilitar la navegación.
                txtRegUser.Text = string.Empty;
                txtRegPass.Text = string.Empty;
            }
            else
            {
                // El registro falla si el usuario ya existe.
                MessageBox.Show(
                    "No se pudo registrar el usuario.\nEl nombre de usuario \"" + username + "\" ya está en uso.",
                    "Error de registro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Botón "Salir": cierra la aplicación sin autenticar.
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

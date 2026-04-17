using System;
using System.Windows.Forms;

namespace Interface_form_
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// Muestra primero el formulario de autenticación; si el usuario
        /// inicia sesión correctamente, abre la ventana principal.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Mostrar el formulario de login antes de abrir la aplicación.
            LoginForm loginForm = new LoginForm();

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // El usuario se autenticó — abrir la ventana principal con su nombre.
                Application.Run(new Main(loginForm.GetLoggedUser()));
            }
            // Si el usuario pulsó "Salir" (DialogResult.Cancel) la app termina aquí.
        }
    }
}

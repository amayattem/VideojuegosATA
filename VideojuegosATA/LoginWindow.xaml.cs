using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace VideojuegosATA
{
    public partial class LoginWindow : Window
    {
        // Credenciales en memoria (Usuario, Contraseña)
        private readonly Dictionary<string, string> usuariosBD = new Dictionary<string, string>
        {
            { "admin", "1234" },
            { "alumno", "dam2026" },
            { "user", "password" }
        };

        // Contador de intentos fallidos por usuario (TC05 / TC07)
        private int intentosFallidos = 0;
        private const int MAX_INTENTOS = 3;
        private bool usuarioBloqueado = false;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // TC05 / TC07: Verificar si la cuenta está bloqueada
            if (usuarioBloqueado)
            {
                MostrarMensaje("El usuario se encuentra bloqueado temporalmente por exceder los intentos.", Colors.OrangeRed);
                return;
            }

            string usuarioIngresado = txtUsuario.Text.Trim();
            string passwordIngresada = txtPassword.Password.Trim();

            // TC 04: No se rellenan los campos
            if (string.IsNullOrEmpty(usuarioIngresado) || string.IsNullOrEmpty(passwordIngresada))
            {
                MostrarMensaje("Por favor, rellene todos los campos del login y contraseña.", Colors.OrangeRed);
                return;
            }

            // TC 03: Inicio de sesión si el usuario no existe
            if (!usuariosBD.ContainsKey(usuarioIngresado))
            {
                MostrarMensaje("El usuario introducido no existe.", Colors.OrangeRed);
                return;
            }

            // TC 01: Inicio de sesión exitoso
            if (usuariosBD[usuarioIngresado] == passwordIngresada)
            {
                intentosFallidos = 0; // Reiniciar contador tras éxito

                // Redirigir a F_Principal (pantalla de bienvenida)
                F_Principal bienvenida = new F_Principal(usuarioIngresado);
                bienvenida.Show();
                this.Close();
            }
            else
            {
                // TC 02: Contraseña incorrecta
                intentosFallidos++;

                // TC 05 / TC 07: Control de 3 intentos fallidos
                if (intentosFallidos >= MAX_INTENTOS)
                {
                    usuarioBloqueado = true;
                    MostrarMensaje("Ha introducido 3 veces mal la contraseña. El usuario ha sido bloqueado temporalmente.", Colors.OrangeRed);
                }
                else
                {
                    int intentosRestantes = MAX_INTENTOS - intentosFallidos;
                    MostrarMensaje($"La contraseña introducida no es correcta. (Intentos restantes: {intentosRestantes})", Colors.OrangeRed);
                }
            }
        }

        private void MostrarMensaje(string mensaje, Color color)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Foreground = new SolidColorBrush(color);
            lblMensaje.Visibility = Visibility.Visible;
        }

        // TC 06: Salir de la aplicación
        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
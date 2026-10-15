using System.Windows;
using System.Windows.Media;

namespace VideojuegosATA
{
    public partial class LoginWindow : Window
    {
        // Credenciales en memoria (Usuario, Contraseña)
        private readonly string[,] usuariosBD = new string[,]
        {
            { "admin", "1234" },
            { "alumno", "dam2026" },
            { "user", "password" }
        };

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuarioIngresado = txtUsuario.Text.Trim();
            string passwordIngresada = txtPassword.Password.Trim();

            // 1. Validar campos vacíos
            if (string.IsNullOrEmpty(usuarioIngresado) || string.IsNullOrEmpty(passwordIngresada))
            {
                MostrarMensaje("Por favor, llena todos los campos.", Colors.OrangeRed);
                return;
            }

            // 2. Comprobar credenciales en la matriz en memoria
            bool esValido = false;
            for (int i = 0; i < usuariosBD.GetLength(0); i++)
            {
                if (usuariosBD[i, 0] == usuarioIngresado && usuariosBD[i, 1] == passwordIngresada)
                {
                    esValido = true;
                    break;
                }
            }

            // 3. Redirigir según el resultado
            if (esValido)
            {
                F_Principal bienvenida = new F_Principal(usuarioIngresado);
                bienvenida.Show();
                this.Close();
            }
            else
            {
                MostrarMensaje("Usuario o contraseña incorrectos.", Colors.Red);
            }
        }

        private void MostrarMensaje(string mensaje, Color color)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Foreground = new SolidColorBrush(color);
            lblMensaje.Visibility = Visibility.Visible;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Cierra toda la aplicación
        }
    }
}
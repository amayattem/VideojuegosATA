using System;
using System.Collections.Generic;
using System.Windows;

namespace VideojuegosATA
{
    public partial class F_Login : Window
    {
        // Diccionario de ejemplo (simulando usuarios registrados en BD)
        private readonly Dictionary<string, string> usuariosRegistrados = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "admin", "1234" },
            { "juan", "juegos2026" },
            { "maria", "gamer123" }
        };

        public F_Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Password;

            // 1. Validar campos vacíos
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                MostrarMensaje("Por favor, rellene todos los campos.", System.Windows.Media.Brushes.OrangeRed);
                return;
            }

            // 2. Verificar si el usuario EXISTE en la base de datos/diccionario
            if (!usuariosRegistrados.ContainsKey(usuario))
            {
                MostrarMensaje("El usuario ingresado no existe.", System.Windows.Media.Brushes.IndianRed);
                txtUsuario.Focus();
                txtUsuario.SelectAll();
                return;
            }

            // 3. Si el usuario existe, verificar si la contraseña coincide
            if (usuariosRegistrados[usuario] == password)
            {
                MostrarMensaje("¡Acceso concedido!", System.Windows.Media.Brushes.LightGreen);

                F_Principal formularioExito = new F_Principal(usuario);
                formularioExito.Show();
                this.Close();
            }
            else
            {
                MostrarMensaje("Contraseña incorrecta.", System.Windows.Media.Brushes.IndianRed);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txtUsuario_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            lblMensaje.Content = string.Empty;
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lblMensaje.Content = string.Empty;
        }

        private void MostrarMensaje(string mensaje, System.Windows.Media.Brush color)
        {
            lblMensaje.Content = mensaje;
            lblMensaje.Foreground = color;
        }


    }
}
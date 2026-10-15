using System;
using System.Windows;
namespace VideojuegosATA{
    public partial class MainWindow : Window
    {
        // Constructor por defecto
        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error XAML en MainWindow:\n\n" + ex.Message);
            }
        }

        // Constructor parametrizado para recibir el usuario
        public MainWindow(string usuario) : this()
        {
            if (!string.IsNullOrWhiteSpace(usuario))
            {
                lblNombreUsuario.Text = usuario;
            }
        }

        // Evento exclusivo del botón de Cerrar Sesión[cite: 9]
        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult respuesta = MessageBox.Show(
                "¿Deseas cerrar sesión y volver a la pantalla de acceso?",
                "Cerrar Sesión",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (respuesta == MessageBoxResult.Yes)
            {
                LoginWindow login = new LoginWindow();
                login.Show();
                this.Close();
            }
        }
    }
}
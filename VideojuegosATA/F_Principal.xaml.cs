using System.Windows;

namespace VideojuegosATA
{
    public partial class F_Principal : Window
    {
        public F_Principal()
        {
            InitializeComponent();
        }

        // Constructor que recibe el usuario desde F_Login
        public F_Principal(string usuario) : this()
        {
            if (!string.IsNullOrWhiteSpace(usuario))
            {
                lblBienvenida.Text = $"¡Bienvenido/a, {usuario}!";
            }
        }

        // ACCIÓN: Al pulsar "Ir al Catálogo", abre MainWindow y cierra F_Principal
        private void btnIrCatalogo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow catalogo = new MainWindow();
            catalogo.Show();
            this.Close(); // Cierra el formulario F_Principal
        }

        // ACCIÓN: Al pulsar "Cerrar Sesión", vuelve a F_Login
        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            F_Login login = new F_Login();
            login.Show();
            this.Close();
        }
    }
}
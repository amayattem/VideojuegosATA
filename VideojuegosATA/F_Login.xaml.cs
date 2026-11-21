using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace VideojuegosATA
{
    public partial class F_Login : Window
    {
        public F_Login()
        {
            InitializeComponent();
        }

        // PASO 4: BOTÓN LOGIN
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Password;

            // 1. Validación de campos vacíos (regresión Práctica 2)[cite: 1]
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, rellena todos los campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 2. Consulta parametrizada a MySQL[cite: 1]
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE nombre_usuario = @usuario AND password = @password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("¡Bienvenido!", "Login correcto", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Pasamos el nombre de usuario al constructor de F_Principal
                            F_Principal principal = new F_Principal(usuario);
                            principal.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.", "Error de acceso", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("No se pudo conectar con la base de datos.\nDetalle: " + ex.Message, "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // PASO 5 y 6: BOTÓN NUEVO USUARIO Y CONTROL DE DUPLICADOS/ERRORES[cite: 1]
        private void btnNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Introduce un usuario y una contraseña para registrarte.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Usuarios (nombre_usuario, password) VALUES (@usuario, @password)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@password", password);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Usuario registrado correctamente.", "Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Error 1062 = Violación de restricción UNIQUE (usuario ya existe)
                if (ex.Number == 1062)
                {
                    MessageBox.Show("El nombre de usuario ya existe. Elige otro.", "Usuario duplicado", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Error de conexión con la base de datos.\nDetalle: " + ex.Message, "Error de MySQL", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtUsuario_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            lblMensaje.Content = "";
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lblMensaje.Content = "";
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    } // <-- Cierra la clase F_Login
} // <-- Cierra el namespace VideojuegosATA
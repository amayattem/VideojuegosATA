using MySql.Data.MySqlClient;

public class DatabaseHelper
{
    // Ajusta el puerto, usuario y contraseña según tu configuración local de MySQL
    private static readonly string connectionString =
        "Server=localhost;Port=3306;Database=catalogo_videojuegos;Uid=root;Pwd=root1234;";

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
}
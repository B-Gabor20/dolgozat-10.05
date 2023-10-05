using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public class Connect
    {
        public MySqlConnection connection;
        private string Host;
        private string DbName;
        private string UserName;
        private string Password;
        private string ConnectionString;

        public Connect()
        {
            Host = "192.168.50.11";
            DbName = "db_jegyek";
            UserName = "root";
            Password = "987654321";

            //ConnectionString = "Host="+Host+";Database="+DbName+";User="+UserName+";Password="+Password+";SslMode=none";

            ConnectionString = $"Host={Host};Database={DbName};User={UserName};Password={Password}";

            connection = new MySqlConnection(ConnectionString);

        }

    }
}
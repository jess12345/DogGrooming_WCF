using System;
using MySql.Data.MySqlClient;
using System.Data;


namespace DogGrooming_WCF
{
    public static class MySqlDatabase
    {
        static string server = "localhost";
        static string databaseName = "doggroomingdatabase";
        static string username = "groomerDatabase";
        static string password = "groomerDatabase";
        
        public static DataTable RunQuery(string sql)
        {
            var connection = new MySqlConnection(String.Concat("SERVER=", server, ";DATABASE=", databaseName, ";UID=", username, ";PASSWORD=", password, ";SslMode=none;"));
            if (connection == null) return null;
            using (MySqlCommand cmd = new MySqlCommand(sql, connection))
            {
                cmd.CommandType = CommandType.Text;
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                using (DataTable dataTable = new DataTable())
                {
                    adapter.Fill(dataTable);
                    connection.Close();
                    return dataTable;
                }
            }
        }
    }
}
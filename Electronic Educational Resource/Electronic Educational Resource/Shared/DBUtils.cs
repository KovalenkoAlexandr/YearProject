using MySql.Data.MySqlClient;

namespace Shared
{
    public class DBUtils
    {
            public static MySqlConnection GetDBConnection()
            {
                string connString = null;
                connString = System.Configuration.ConfigurationManager.
                         ConnectionStrings["ServerApplicationConnection"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connString);
                return conn;
            }
    }
}

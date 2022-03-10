using System.Data.SqlClient;

namespace DapperSimpleTest.Data
{
    static class Connection
    {
        static SqlConnection _connection = null;
        public static SqlConnection GetConnection()
        {
            if (_connection == null)
                return _connection = new SqlConnection("Server=localhost,1433;Database=DapperTest;User Id=x;Password=xx");
            else
                return _connection;
        }

    }
}
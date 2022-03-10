using System.Data.SqlClient;

namespace DapperSimpleTest.Data
{
    static class Connection
    {
        private static SqlConnection _connection = null;
        public static SqlConnection GetConnection()
        {
            return _connection = new SqlConnection("Server=localhost,1433;Database=DapperTest;User Id=sa;Password=Kkld@123#$");
        }
    }
}
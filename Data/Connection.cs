using System.Data.SqlClient;

namespace DapperSimpleTest.Data
{
    static class Connection
    {
        private static SqlConnection _connection = null;
        public static SqlConnection GetConnection()
        {
            return _connection = new SqlConnection(@"Server=pc\sqlexpress;Database=DapperTest;Trusted_Connection=true");
        }
    }
}
using Microsoft.Data.SqlClient;

namespace Awesome.UnitTest.Infrastructures.Db
{
    public class SQLDBConnections
    {
        public readonly SqlConnection DbConnection = new SqlConnection("Server=localhost;Database=AwesomeDBTest;Integrated Security=True;TrustServerCertificate=True;");
    }
}
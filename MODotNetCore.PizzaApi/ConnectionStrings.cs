using System.Data.SqlClient;

namespace MODotNetCore.PizzaApi;

public class ConnectionStrings
{
    public static SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder()
    {
        DataSource = "DESKTOP-QIPPQBI\\SQLEXPRESS",
        InitialCatalog = "DotNetTrainingBatch4",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true
    };
}

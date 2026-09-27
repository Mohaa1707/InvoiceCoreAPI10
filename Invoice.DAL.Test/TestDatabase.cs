using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.DAL.Test;

public static class TestDatabase
{

    public static string ConnectionString =>

        Environment.GetEnvironmentVariable("TEST_DB_CONNECTION")

        ?? "Server=Mohanrajmurugan\\SQLEXPRESS,1435;" +

           "Database=Invoice_Test;" +

           "User Id=sa;" +

           "Password=Mohan;" +

           "Encrypt=False;" +

           "TrustServerCertificate=True";

}

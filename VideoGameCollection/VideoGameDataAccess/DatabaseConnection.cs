using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameDataAccess
{
    internal static class DatabaseConnection
    {
        internal static SqlConnection RetrieveDatabaseConnection()
        {
            var connString = @"Data Source=localhost;Initial Catalog=VideoGameDatabase;Integrated Security=True";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}

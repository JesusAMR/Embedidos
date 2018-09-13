using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace test.Controllers
{
    public static class Conneccion
    {
        private static readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
        {
            DataSource = "embedidostest.database.windows.net",
            UserID = "jesus",
            Password = "",
            InitialCatalog = "dbTest"
        };

        public static bool Execute(StringBuilder query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = query.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public static DataTable ExecuteDataTable(StringBuilder query)
        {
            DataTable Valores = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = query.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Valores.Load(reader);
                            return Valores;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
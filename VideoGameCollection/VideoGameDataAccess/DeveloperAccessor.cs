using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameDataAccess
{
    public class DeveloperAccessor
    {
        public static List<string> RetrieveDevelopers(bool active)
        {
            var developers = new List<string>();

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_retrieve_developers";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@IS_APPROVED", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        developers.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return developers;
        }

        public static int CreateDeveloper(string developer, bool active)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_create_developer";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@NAME", developer);
            cmd.Parameters.AddWithValue("@IS_APPROVED", active);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public static int DeleteDeveloper(string developer)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_delete_developer";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@NAME", developer);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public static int ApproveDeveloper(string developer)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_approve_developer";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@NAME", developer);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

    }

}

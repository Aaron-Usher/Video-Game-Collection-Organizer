using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameDataAccess
{
    public class ConsoleAccessor
    {
        public static List<string> RetrieveConsoles(bool active)
        {

            var consoles = new List<string>();

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_retrieve_consoles";
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
                        consoles.Add(reader.GetString(0));
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
            return consoles;
        }

        public static int CreateConsole(string console, bool active)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_create_console";
            var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure};

            cmd.Parameters.AddWithValue("@NAME", console);
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

        public static int DeleteConsole(string console)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_delete_console";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@NAME", console);

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

        public static int ApproveConsole(string console)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_approve_console";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@NAME", (object)console ?? DBNull.Value);

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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameDataObjects;

namespace VideoGameDataAccess
{
    public static class UserAccessor
    {
        public static int CreateUser(User user)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_create_user";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@USER_ID", user.Username);

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

        public static int CreateUserRole(string username, string role)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_create_user_role";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@USER_ID", username);
            cmd.Parameters.AddWithValue("@ROLE_ID", role);

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

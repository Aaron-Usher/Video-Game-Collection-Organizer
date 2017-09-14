using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameDataAccess
{
    public class PublisherAccessor
    {
        public static List<string> RetrievePublishers(bool active)
        {

            var publishers = new List<string>();

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_retrieve_publishers";
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
                        publishers.Add(reader.GetString(0));
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
            return publishers;


        }

        public static int CreatePublisher(string publisher, bool active)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_create_publisher";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@NAME", publisher);
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

        public static int DeletePublisher(string publisher)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_delete_publisher";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@NAME", publisher);

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

        public static int ApprovePublisher(string publisher)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_approve_publisher";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@NAME", publisher);

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

        public static int CountPublisher(string publisher)
        {
            var count = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_count_publisher";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@PUBLISHER_ID", publisher);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

    }
}

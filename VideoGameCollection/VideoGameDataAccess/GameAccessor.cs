using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameDataObjects;

namespace VideoGameDataAccess
{
    public static class GameAccessor
    {
        public static List<Game> RetrieveUserGames(string username)
        {
            var games = new List<Game>();

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_retrieve_user_games";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@USERNAME", username);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        games.Add(new Game()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Console = reader.GetString(2),
                            Developer = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Publisher = reader.IsDBNull(4) ? null : reader.GetString(4),
                            IsOwned = true
                        });
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
            return games;
        }

        public static Game RetrieveGame(int gameId)
        {
            Game game = null;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_retrieve_game";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@GAME_ID", gameId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    game = new Game()
                    {

                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Console = reader.GetString(2),
                        Developer = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Publisher = reader.IsDBNull(4) ? null : reader.GetString(4),
                        IsOwned = false
                    };
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
            return game;
        }

        public static List<Game> RetrieveGames(bool active)
        {
            var games = new List<Game>();

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_retrieve_games";
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
                        games.Add(new Game()
                        {

                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Console = reader.GetString(2),
                            Developer = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Publisher = reader.IsDBNull(4) ? null : reader.GetString(4),
                            IsOwned = false
                        });
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
            return games;
        }

        public static int CreateGame(Game game, bool active)
        {
            int gameId = -1;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_create_game";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@NAME", game.Name);
            cmd.Parameters.AddWithValue("@CONSOLE", game.Console);
            cmd.Parameters.AddWithValue("@DEVELOPER", (object)game.Developer ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PUBLISHER", (object)game.Publisher ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IS_APPROVED", active);
            try
            {
                conn.Open();
                gameId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return gameId;
        }

        public static int CreateUserGame(int gameId, string username)
        {
            int rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_create_user_game";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@GAME_ID", gameId);
            cmd.Parameters.AddWithValue("@USER_ID", username);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
        
        public static int DeleteGame(int gameId)
        {
            int rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_delete_game";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@GAME_ID", gameId);

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

        public static int DeleteUserGame(int gameId, string username)
        {
            int rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_delete_user_game";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@GAME_ID", gameId);
            cmd.Parameters.AddWithValue("@USER_ID", username);

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

        public static int UpdateGame(Game oldGame, Game newGame)
        {
            int rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_update_game";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@GAME_ID", oldGame.Id);

            cmd.Parameters.AddWithValue("@OLD_NAME", oldGame.Name);
            cmd.Parameters.AddWithValue("@OLD_CONSOLE", oldGame.Console);
            cmd.Parameters.AddWithValue("@OLD_DEVELOPER", (object)oldGame.Developer ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@OLD_PUBLISHER", (object)oldGame.Publisher ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@NEW_NAME", newGame.Name);
            cmd.Parameters.AddWithValue("@NEW_CONSOLE", newGame.Console);
            cmd.Parameters.AddWithValue("@NEW_DEVELOPER", (object)newGame.Developer ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NEW_PUBLISHER", (object)newGame.Publisher ?? DBNull.Value);

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

        public static int ApproveGame(int gameId)
        {
            var rows = 0;

            var conn = DatabaseConnection.RetrieveDatabaseConnection();
            var cmdText = @"vsp_approve_game";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@GAME_ID", gameId);

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

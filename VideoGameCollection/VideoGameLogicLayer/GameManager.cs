using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameDataAccess;
using VideoGameDataObjects;

namespace VideoGameLogicLayer
{
    public static class GameManager
    {

        public static List<Game> RetrieveUserGames(string username)
        {

            List<Game> games = null;

            try
            {
                games = GameAccessor.RetrieveUserGames(username);
            }
            catch (Exception)
            {

                throw;
            }
            return games;
        }

        public static List<Game> RetrieveGames(bool active = true)
        {
            List<Game> games = null;
            try
            {
                games = GameAccessor.RetrieveGames(active);
            }
            catch (Exception)
            {

                throw;
            }


            return games;
        }

        public static Game RetrieveGame(int gameId)
        {
            Game game = null;

            try
            {
                game = GameAccessor.RetrieveGame(gameId);
            }
            catch (Exception)
            {

                throw;
            }

            return game;
        }


        public static List<Game> RetrieveGamesWithOwnershipStatus(string username)
        {
            List<Game> games = RetrieveGames();
            List<Game> userGames = RetrieveUserGames(username);
            foreach (var game in games)
            {
                game.IsOwned = userGames.Exists(g => g.Id == game.Id);
            }
            return games;
        }

        public static bool CreateGame(Game game, string username = null, bool admin = false)
        {
            bool result = false;

            try
            {

                if (!string.IsNullOrEmpty(game.Publisher) && !PublisherManager.RetrievePublishers().Contains(game.Publisher.Trim()))
                {
                    if (!PublisherManager.CreatePublisher(game.Publisher, admin))
                    {
                        throw new ApplicationException("Publisher failed to write!");
                    }

                }
                if (!string.IsNullOrEmpty(game.Developer) && !DeveloperManager.RetrieveDevelopers().Contains(game.Developer.Trim()))

                {
                    if (!DeveloperManager.CreateDeveloper(game.Developer, admin))
                    {
                        throw new ApplicationException("Developer failed to write!");
                    }

                }
                if (!string.IsNullOrEmpty(game.Console) && !ConsoleManager.RetrieveConsoles().Contains(game.Console.Trim()))
                {
                    if (!ConsoleManager.CreateConsole(game.Console, admin))
                    {
                        throw new ApplicationException("Console failed to write!");
                    }

                }
                int gameId = GameAccessor.CreateGame(game, admin);
                //If an admin called this with the admin parameter, don't add it to their collection.
                if (username != null && admin == false)
                {
                    GameAccessor.CreateUserGame(gameId, username);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public static bool UpdateGame(Game oldGame, Game newGame)
        {
            bool result = false;
            try
            {
                if (GameAccessor.RetrieveGame(oldGame.Id) != oldGame)
                {
                    throw new ApplicationException("Game has been updated since last retrieval!");
                }
                else
                {
                    if (!DeveloperManager.UpdateDeveloper(newGame.Developer))
                    {
                        throw new ApplicationException("Developer failed to update!");
                    }
                    if (!ConsoleManager.UpdateConsole(newGame.Console))
                    {
                        throw new ApplicationException("Console failed to update!");
                    }
                    if (!PublisherManager.UpdatePublisher(newGame.Publisher))
                    {
                        throw new ApplicationException("Publisher failed to update!");
                    }
                    result = 1 == GameAccessor.UpdateGame(oldGame, newGame);
                    ConsoleManager.VerifyConsole(oldGame.Console);
                    DeveloperManager.VerifyDeveloper(oldGame.Developer);
                    PublisherManager.VerifyPublisher(oldGame.Publisher);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
        public static bool UpdateUserGames(string username, List<Game> gamesToSet)
        {
            bool result = true;

            foreach (var game in gamesToSet)
            {
                if (game.IsOwned)
                {
                    GameAccessor.CreateUserGame(game.Id, username);
                }
                else
                {
                    GameAccessor.DeleteUserGame(game.Id, username);
                }
            }

            return result;
        }

        public static bool ToggleUserGame(int gameId, string username)
        {
            bool result = false;

            try
            {
                bool created = 1 == GameAccessor.CreateUserGame(gameId, username);
                if (created)
                {
                    result = created;
                }
                else
                {
                    result = 1 == GameAccessor.DeleteUserGame(gameId, username);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        //Also replace this with a better stored procedure if time permits.
        public static bool CheckUserGame(int gameId, string username)
        {
            bool result = false;

            try
            {
                result = RetrieveUserGames(username).Any(g => g.Id == gameId);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public static bool DeleteGame(int gameId)
        {
            bool result = false;
            try
            {
                var game = GameAccessor.RetrieveGame(gameId);
                GameAccessor.DeleteGame(gameId);
                ConsoleManager.VerifyConsole(game.Console);
                DeveloperManager.VerifyDeveloper(game.Developer);
                PublisherManager.VerifyPublisher(game.Publisher);
               
            }
            catch
            {
                throw;
            }
            return result;
        }

        public static bool ApproveGame(int gameId)
        {
            bool result = false;
            try
            {
                var game = RetrieveGame(gameId);
                if (!string.IsNullOrEmpty(game.Publisher))
                {
                    PublisherManager.ApprovePublisher(game.Publisher);
                }
                if (!string.IsNullOrEmpty(game.Developer))
                {
                    DeveloperManager.ApproveDeveloper(game.Developer);
                }
                if (!string.IsNullOrEmpty(game.Console))
                {
                    ConsoleManager.ApproveConsole(game.Console);
                }
               
                GameAccessor.ApproveGame(gameId);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
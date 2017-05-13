using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameDataAccess;
using VideoGameDataObjects;

namespace VideoGameLogicLayer
{
    public static class DeveloperManager
    {
        public static List<string> RetrieveDevelopers(bool active = true)
        {
            List<string> developers = null;

            try
            {
                developers = DeveloperAccessor.RetrieveDevelopers(true);
                
            }
            catch (Exception)
            {

                throw;
            }

            return developers;
        }

        public static bool CreateDeveloper(string developer, bool admin = false)
        {
            bool result = false;

            try
            {
                result = 1 == DeveloperAccessor.CreateDeveloper(developer, admin);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public static bool UpdateDeveloper(string newDeveloper)
        {
            bool result = false;
            List<Game> games = GameManager.RetrieveAllGames();
            if (games.Count(g=> g.Developer == newDeveloper) == 0)
            {
                result = 1 == DeveloperAccessor.CreateDeveloper(newDeveloper, true);
            }
            else
            {
                result = true;
            }
            
            return result;
        }

        public static bool VerifyDeveloper(string oldDeveloper)
        {
            bool result = true;
            if (string.IsNullOrEmpty(oldDeveloper))
            {
                return result;
            }
            List<Game> games = GameManager.RetrieveAllGames();
            if (games.Count(g => g.Developer == oldDeveloper) == 0)
            {
                result = 1 == DeveloperAccessor.DeleteDeveloper(oldDeveloper);
            }

            return result;
        }

        public static bool ApproveDeveloper(string developer)
        {
            bool result = false;

            try
            {
                DeveloperAccessor.ApproveDeveloper(developer);
                result = true;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

    }
}

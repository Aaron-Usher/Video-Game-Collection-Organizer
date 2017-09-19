using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameDataAccess;
using VideoGameDataObjects;

namespace VideoGameLogicLayer
{
    public class ConsoleManager
    {
        /// <summary>
        /// All of the retrieve methods take an optional "active" parameter; basically, you can pass it a false
        /// in order to get all of the unapproved items.
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public static List<string> RetrieveConsoles(bool active = true)
        {
            List<string> consoles = null;

            try
            {
                consoles = ConsoleAccessor.RetrieveConsoles(active);
            }
            catch (Exception)
            {

                throw;
            }

            return consoles;
        }

        public static bool CreateConsole(string console, bool admin = false)
        {
            bool result = false;

            try
            {
                result = 1 == ConsoleAccessor.CreateConsole(console, admin);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        
        // Due to the way the database and foreign keys and all that work, the Update methods for these basically had to be split out
        // into two seperate methods that had to be called before and after the actual updating of a game. This used to delete the old console if
        // it wasn't needed, and then add the new console if it was; the new console has to be there for the other update to work, but you
        // can't delete a foreign key while it is still referenced, so into seperate methods they went.
        public static bool UpdateConsole(string newConsole)
        {
            bool result = true;
  
            if (CountConsole(newConsole) == 0)
            {
                result = 1 == ConsoleAccessor.CreateConsole(newConsole, true);
            }

            return result;
        }

        public static bool VerifyConsole(string oldConsole)
        {
            bool result = true;
            if (!string.IsNullOrEmpty(oldConsole) && CountConsole(oldConsole) == 0)
            {
                result = 1 == ConsoleAccessor.DeleteConsole(oldConsole);
            }
            return result;
        }

        public static bool ApproveConsole(string console)
        {
            bool result = false;

            try
            {
                ConsoleAccessor.ApproveConsole(console);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public static int CountConsole(string console)
        {
            int count = 0;

            try
            {
                count = ConsoleAccessor.CountConsole(console);
            }
            catch(Exception)
            {
                throw;
            }

            return count;
        }

    }
}

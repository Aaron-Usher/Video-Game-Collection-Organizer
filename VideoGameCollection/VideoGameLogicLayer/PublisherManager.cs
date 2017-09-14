using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameDataAccess;
using VideoGameDataObjects;

namespace VideoGameLogicLayer
{
    public class PublisherManager
    {
        public static List<string> RetrievePublishers(bool active = true)
        {
            List<string> publishers = null;

            try
            {
                publishers = PublisherAccessor.RetrievePublishers(active);
            }
            catch (Exception)
            {

                throw;
            }

            return publishers;
        }

        public static bool CreatePublisher(string publisher, bool admin = false)
        {
            bool result = false;

            try
            {
                result = 1 == PublisherAccessor.CreatePublisher(publisher, admin);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public static bool UpdatePublisher(string newPublisher)
        {
            bool result = true;

            if (CountPublisher(newPublisher) == 0)
            {
                result = 1 == PublisherAccessor.CreatePublisher(newPublisher, true);
            }
            
            return result;
        }

        public static bool VerifyPublisher(string oldPublisher)
        {
            bool result = true;
            if (string.IsNullOrEmpty(oldPublisher))
            {
                return true;
            }
            if (CountPublisher(oldPublisher) == 0)
            {
                result = 1 == PublisherAccessor.DeletePublisher(oldPublisher);
            }

            return result;
        }

        public static bool ApprovePublisher(string publisher)
        {
            bool result = false;

            try
            {
                PublisherAccessor.ApprovePublisher(publisher);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public static int CountPublisher(string publisher)
        {
            int count = 0;

            try
            {
                count = PublisherAccessor.CountPublisher(publisher);
            }
            catch (Exception)
            {
                
                throw;
            }

            return count;
        }

    }
}

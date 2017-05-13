using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameDataObjects;
using VideoGameDataAccess;

namespace VideoGameLogicLayer
{
    public class UserManager
    {
        public bool RegisterUser(User user)
        {
            bool result = false;

            try
            {
                bool userCreated =  1 == UserAccessor.CreateUser(user);
                if (!userCreated)
                {
                    throw new ApplicationException("User could not be created!");
                }
                else
                {
                    foreach (var role in user.Roles)
                    {
                        if (1 != UserAccessor.CreateUserRole(user.Username, role))
                        {
                            result = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return result;
        }
    }
}

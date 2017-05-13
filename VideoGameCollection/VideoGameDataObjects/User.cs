using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameDataObjects
{
    public class User
    {
        public string Username { get; set; }
        public List<string> Roles { get; set; }

        public User()
        {
            Roles = new List<string>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class User
    {
        public int UserID { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }

    public class UserVM : User
    { 
        public List<string> Roles { get; set; } = new List<string>();
        public List<int> CollectionIDs { get; set; } = new List<int>();
    }
}

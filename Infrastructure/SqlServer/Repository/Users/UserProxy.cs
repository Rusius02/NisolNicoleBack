using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SqlServer.Repository.Users
{
    public class UserProxy
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string token { get; set; }
        public string sexe { get; set; }
        public DateTime birthDate { get; set; }
        public string mail { get; set; }

        public string pseudo { get; set; }

        public string role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT.Models
{
    public class Profile
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public int status { get; set; }
        public bool role { get; set; }
        public DateTime date_reg { get; set; }
    }
}
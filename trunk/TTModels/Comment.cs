using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTModels
{
    public class Comments
    {
        public int id { get; set; }
        public int item_id { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public DateTime dateposted { get; set; }
    }
}
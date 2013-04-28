using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT.Models
{
    public class Comment
    {
        public int id { get; set; }
        public int item_id { get; set; }
        public string ip { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public DateTime datetime { get; set; }
    }
}
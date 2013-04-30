using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT.Models
{
    public class PostUserCombined
    {
        public Posts Post { get; set; }
        public UserProfile Profile { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }
}
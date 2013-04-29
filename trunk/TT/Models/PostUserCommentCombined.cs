using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT.Models
{
    public class PostUserCombined
    {
        public Post Post { get; set; }
        public UserProfile Profile { get; set; }
    }
}
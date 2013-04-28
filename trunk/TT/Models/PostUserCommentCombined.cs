using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT.Models
{
    public class PostUserCommentCombined
    {
        public Post Post { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Profile Profile { get; set; }
        public double Width { get; set; }
        public double Heigth { get; set; }
    }
}
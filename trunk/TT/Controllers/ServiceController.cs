using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TT.Models;

namespace TT.Controllers
{
    public class ServiceController : Controller
    {
        //
        // GET: /Service/

        public ActionResult Index(int id = -1)
        {
            if (id == -1)
                return HttpNotFound();

            var db = new DBModel();
            return View(db.GetPostCommentsAndUser(id));
        }

        public ActionResult Comments(int id = -1)
        {
            if (id == -1)
                return HttpNotFound();

            var db = new DBModel();
            return View(db.GetComments(id));
        }

        [HttpPost]
        public string PostCommentAjax(int id, string comment)
        {
            if (id == -1 || String.IsNullOrEmpty(comment))
                return "-1";

            var db = new DBModel();
            var c = db.PostComment(id, comment, User.Identity.Name);

            return "<div class=\"comment\" id=\"" + c.id + "\"><p>Comment from " + c.name + " <span>" + c.dateposted + "</span>:</p><p>" + c.text + "</p></div>";
        }

        [HttpPost]
        public void PostComment(int id, string comment)
        {
            if (id == -1 || String.IsNullOrEmpty(comment))
                return;

            var db = new DBModel();
            var c = db.PostComment(id, comment, User.Identity.Name);
        }
    }
}

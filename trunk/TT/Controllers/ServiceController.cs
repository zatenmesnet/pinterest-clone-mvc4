using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TT.Models;
using System.IO;
using WebMatrix.WebData;

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

        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public void Upload()
        {
            // Loop through each file in the request
            for (int i = 0; i < HttpContext.Request.Files.Count; i++)
            {
                // Pointer to file
                var file = HttpContext.Request.Files[i];

                // Save file to server
                var fullPath = @"C:\Users\bradley\Documents\Visual Studio 2010\Projects\TT\TT\photos\" + file.FileName;
                file.SaveAs(fullPath);

                var db = new DBModel();
                db.PostPost("$" + HttpContext.Request.Form["symbol"], @".\photos\" + file.FileName, WebSecurity.GetUserId(User.Identity.Name), DateTime.UtcNow);
            }
        }

        public ActionResult TradingView(string symbol)
        {
            ViewBag.Symbol = "FX:" + symbol;

            return View();
        }
    }
}

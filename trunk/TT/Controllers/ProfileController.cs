using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TT.Models;

namespace TT.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public ActionResult Index(int id = -1)
        {
            if (id == -1)
                return HttpNotFound();

            var db = new DBModel();
            ViewBag.ProfileName = db.GetProfile(id).email;
            return View(db.GetPosts(id));
        }

    }
}

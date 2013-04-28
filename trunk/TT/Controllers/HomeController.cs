using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TT.Models;
using System.Web.Security;

namespace TT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new DBModel();

            return View(db.GetPosts());
        }
    }
}

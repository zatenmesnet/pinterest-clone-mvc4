using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int start = 0, int limit = 50)
        {
            if (start == 0)
                return View(new TTRESTService().GetPosts(start, limit));
            else
                return PartialView(new TTRESTService().GetPosts(start, limit));
        }
    }
}

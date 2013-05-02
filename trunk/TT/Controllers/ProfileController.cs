using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TT.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public ActionResult Index(int id = -1)
        {
            return View(new TTRESTService().GetPosts(id));
        }
    }
}

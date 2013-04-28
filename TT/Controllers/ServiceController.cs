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

    }
}

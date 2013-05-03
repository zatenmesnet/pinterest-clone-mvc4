using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WebMatrix.WebData;
using TTModels;

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

            return View(new TTRESTService().GetPostUser(id));
        }

        public ActionResult Comments(int id = -1)
        {
            if (id == -1)
                return HttpNotFound();

            return View(new TTRESTService().GetComments(id));
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
                Posts p = new Posts() { title = "$" + HttpContext.Request.Form["tags"], filename = @".\photos\" + file.FileName, owner = WebSecurity.GetUserId(User.Identity.Name), dateuploaded = DateTime.UtcNow };

                new TTRESTService().PostPost(p);
            }
        }

        public ActionResult TradingView(string symbol)
        {
            ViewBag.Symbol = "FX:" + symbol;

            return View();
        }
    }
}
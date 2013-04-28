using System.Web;
using System.Web.Optimization;

namespace TT
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery.js", 
                "~/Scripts/jquery.masonry.js", 
                "~/Scripts/jquery.colorbox.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/sitescript").Include(
                "~/Scripts/script.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/main.css",
                "~/Content/colorbox.css" ));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thinktecture.IdentityModel.Http.Cors.WebApi;
using System.Web.Http;

namespace TTREST.App_Start
{
    public class CorsConfig
    {
        //http://brockallen.com/2012/06/28/cors-support-in-webapi-mvc-and-iis-with-thinktecture-identitymodel/
        public static void RegisterCors(HttpConfiguration httpConfig)
        {
            WebApiCorsConfiguration corsConfig = new WebApiCorsConfiguration();
 
            // this adds the CorsMessageHandler to the HttpConfiguration’s
            // MessageHandlers collection
            corsConfig.RegisterGlobal(httpConfig);
       
            corsConfig.AllowAll();
        }
    }
}
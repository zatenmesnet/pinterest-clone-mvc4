using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TTModels;

namespace TTREST.Controllers
{
    public class ProfileController : ApiController
    {
        // GET api/profile/5
        public UserProfile Get(int id)
        {
            var db = new DBModel();
            return new DBModel().GetProfile(id);
        }

        // POST api/profile
        public void Post([FromBody]string value)
        {
        }

        // PUT api/profile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/profile/5
        public void Delete(int id)
        {
        }
    }
}

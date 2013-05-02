using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TTModels;

namespace TTREST.Controllers
{
    public class PostAndUserController : ApiController
    {
        // GET api/postanduser
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/postanduser/5
        public PostUserCombined Get(int id)
        {
            return new DBModel().GetPostCommentsAndUser(id);
        }

        // POST api/postanduser
        public void Post([FromBody]string value)
        {
        }

        // PUT api/postanduser/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/postanduser/5
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TTModels;

namespace TTREST.Controllers
{
    public class PostsController : ApiController
    {
        // GET api/posts
        public IEnumerable<Posts> Get()
        {
            var db = new DBModel();

            return new DBModel().GetPosts();
        }

        // GET api/posts/5
        public IEnumerable<Posts> Get(int id)
        {
            return new DBModel().GetPosts(id);
        }

        // POST api/posts
        public void Post([FromBody]string value)
        {
        }

        // PUT api/posts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/posts/5
        public void Delete(int id)
        {
        }
    }
}

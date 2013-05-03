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
        public IEnumerable<Posts> GetAllPosts()
        {
            return new DBModel().GetPosts();
        }

        public IEnumerable<Posts> GetRange(int start, int limit)
        {
            return new DBModel().GetPosts(start, limit);
        }

        // GET api/posts/5
        // Gets a users posts
        public IEnumerable<Posts> GetUsersPosts(int id)
        {
            return new DBModel().GetPosts(id);
        }

        // POST api/posts
        public void Post([FromBody]Posts p)
        {
            new DBModel().PostPost(p.title, p.filename, p.owner, p.dateuploaded);
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

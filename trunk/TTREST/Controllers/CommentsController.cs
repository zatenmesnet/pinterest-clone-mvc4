using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TTModels;

namespace TTREST.Controllers
{
    public class CommentsController : ApiController
    {
        // GET api/comments/5
        public IEnumerable<Comments> Get(int id)
        {
            return new DBModel().GetComments(id);
        }

        // POST api/comments
        public void Post(int id, [FromBody]Comments c)
        {
            new DBModel().PostComment(id, c.text, c.name);
        }

        // PUT api/comments/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/comments/5
        public void Delete(int id)
        {
        }
    }
}

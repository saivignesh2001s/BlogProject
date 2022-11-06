using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogProjectWebApi.Repositories;
using DAL;
using Newtonsoft.Json.Linq;

namespace BlogProjectWebApi.Controllers
{
    public class BlogController : ApiController
    {
        BlogRepository blog = null;
        public BlogController()
        {
            blog = new BlogRepository();
        }
        // GET: api/Blog
        [Route("GetAllblog")]
        public IEnumerable<BlogInfo> Get()
        {
            return blog.GetAll();
        }

        [Route("GetByblog/{id}")]
        // GET: api/Blog/5
        public BlogInfo Get(int id)
        {
            return blog.GetById(id);
        }

        // POST: api/Blog
        [Route("Insertblog")]
        public HttpResponseMessage Post([FromBody]BlogInfo value)
        {
            bool k = blog.insert(value);
            if (k)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

        }

        // PUT: api/Blog/5
        [Route("Updateblog/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]BlogInfo value)
        {
            bool k = blog.update(id,value);
            if (k)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }

        // DELETE: api/Blog/5
        [Route("Deleteblog/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            bool k = blog.delete(id);
            if (k)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL;
using BlogProjectWebApi.Repositories;
namespace BlogProjectWebApi.Controllers
{
    public class EmployeeController : ApiController
    {

        EmpRepository emp = null;
      
        public EmployeeController()
        {
            emp=new EmpRepository();
        }
        // GET: api/Employee
        [Route("GetAllEmps")]
        public IEnumerable<EmpInfo> GetAll()
        {
            return emp.GetAll();         
        }

        // GET: api/Employee/5
        [Route("GetEmp/value/{value}")]
        public EmpInfo Get(string value)
        {
            return emp.Get(value);
        }

        // POST: api/Employee
        [Route("InsertEmp")]
        public HttpResponseMessage Post([FromBody]EmpInfo value)
        {
            bool k=emp.insert(value);
            if (k)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

        }

        [Route("VerifyEmp/id/{id}/value/{value}")]        
        public EmpInfo Get(int id,string value)
        {
            EmpInfo k= emp.Verify(value,id);
            return k;

        }
    }
}

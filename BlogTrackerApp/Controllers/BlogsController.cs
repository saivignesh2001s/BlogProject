using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DAL;
using BlogTrackerApp.Models;
using BlogProjectWebApi.Repositories;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;

namespace BlogTrackerApp.Controllers
{
    public class BlogsController : Controller
    {
        adminmethods m1= null;
        MyContext sm=null;
        BlogRepository r = null;
        EmpRepository rk = null;
        public BlogsController()
        {
            m1 = new adminmethods();
            sm = new MyContext();
            r = new BlogRepository();
            rk = new EmpRepository();
        }
        public ActionResult Index()
        {
            List<Blog> m = new List<Blog>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44313/GetAllblog");
                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<Blog[]>();
                    readData.Wait();
                    var m1= readData.Result;
                    foreach(var item in m1)
                    {
                        Blog info = new Blog(); 
                        info.BlogId=item.BlogId;
                        info.Title=item.Title;
                        info.DateOfCreation=item.DateOfCreation;
                        info.BlogUrl = item.BlogUrl;
                        info.Subject = item.Subject;
                        info.Emailid = item.Emailid;
                        m.Add(info);
                    }
                }


            }
            return View(m);
        }

        public ActionResult AdminLogin()
        {
           

            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(FormCollection collection)
        {
            AdminInfo m = new AdminInfo();
            m.Emailid = collection["EmailId"].ToString();
            m.Password = collection["password"].ToString();
            bool k = m1.CheckLogin(m);
            if (k)
            {
                return RedirectToAction("EmployeeList");
            }
            else
            {
                ViewBag.Message = "Invalid Credentials..TryAgain";
                return View();
            }
        }
        public ActionResult EmployeeList()
        {
            List<Empl> m1 = new List<Empl>();
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44313/GetAllEmps");
                   var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<Empl[]>();
                    readData.Wait();
                    var m2 = readData.Result;
                    foreach(var item in m2) {
                        Empl m = new Empl();
                        m.Emailid = item.Emailid;
                        m.Name = item.Name;
                        m.Passcode = item.Passcode;
                        m.DateOfJoining = item.DateOfJoining;
                        m1.Add(m);
                    
                    }
                }

                }
            return View(m1);
        }

        public ActionResult NewEmployee()
        {

            return View();
        }
        [HttpPost]
        public ActionResult NewEmployee(Empl emp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44313/InsertEmp");

                var EmpInfo = new EmpInfo {EmailId=emp.Emailid,Passcode=emp.Passcode,DateOfJoining=emp.DateOfJoining,Name=emp.Name};

                var postTask = client.PostAsJsonAsync<EmpInfo>(client.BaseAddress,EmpInfo);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtaskResult = result.Content.ReadAsAsync<Empl>();

                    readtaskResult.Wait();
                    var dataInserted = readtaskResult.Result;
                    return RedirectToAction("EmployeeList");
                }
                else
                {
                    return View();
                }

            }
           

        }
        public ActionResult EmpLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmpLogin(FormCollection collection)
        {
            string value = collection["Emailid"];
            int id = Convert.ToInt32(collection["Passcode"]);
            bool k = false;
            foreach(var item in sm.EmpInfoes.ToList())
            {
                if(item.EmailId==value && id == item.Passcode)
                {
                    k = true;
                }

            }
               if (k)
                    {
                        TempData["user"] = value.ToString();
                        return RedirectToAction("IndBlogList");
                    }
                    else
                    {
                        ViewBag.Message1 = "Invalid Credentials..Try Again";
                        return View();
                    }
          
            
        }
        public ActionResult IndBlogList()
        {
            List<Blog> m = new List<Blog>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44313/GetAllblog");
                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<Blog[]>();
                    readData.Wait();
                    var m1 = readData.Result;
                    string m2 = TempData["user"].ToString();
                    TempData["user"] = m2;
                    foreach (var item in m1)
                    {
                        if (item.Emailid == m2)
                        {

                            Blog info = new Blog();
                            info.BlogId = item.BlogId;
                            info.Title = item.Title;
                            info.DateOfCreation = item.DateOfCreation;
                            info.BlogUrl = item.BlogUrl;
                            info.Subject = item.Subject;
                            info.Emailid = item.Emailid;
                            m.Add(info);
                        }
                    }
                }


            }
            return View(m);
        }
        public ActionResult NewBlog()
        {
            Blog m = new Blog();
            m.Emailid = TempData["user"].ToString().ToLower(); ;
            TempData["user"] = m.Emailid;
            return View(m);
        }
        [HttpPost]
        public ActionResult NewBlog(FormCollection c)
        {
            BlogInfo s1 = new BlogInfo();
            s1.BlogId = Convert.ToInt32(c["BlogId"]);
            s1.Subject = c["Subject"].ToString();
            s1.Title = c["Title"].ToString();
            s1.BlogUrl = c["BlogUrl"].ToString();
            s1.DateOfCreation = Convert.ToDateTime(c["DateOfCreation"]);
            s1.Emailid = c["Emailid"].ToString();
            bool k = r.insert(s1);
            if (k)
            {
                return RedirectToAction("IndBlogList");
            }
            else
            {
                return View();
            }

           
        }
        public ActionResult UpdateBlog(int id)
        {
            
                   Blog s=new Blog();
            BlogInfo m2 = r.GetById(id);
                    s.BlogId = Convert.ToInt32(m2.BlogId);
                    s.DateOfCreation = m2.DateOfCreation;
                    s.Subject = m2.Subject;
                    s.Title = m2.Title;
                    s.BlogUrl = m2.BlogUrl;
                    s.Emailid = m2.Emailid;

          
            return View(s);



        }
        [HttpPost]
        public ActionResult UpdateBlog(int id,Blog b)
        {
            BlogInfo s = new BlogInfo();
            s.Subject=b.Subject;
            s.BlogId = b.BlogId;
            s.BlogUrl = b.BlogUrl;
            s.DateOfCreation = b.DateOfCreation;
            s.Emailid = b.Emailid;
            s.Title=b.Title;
            bool k=r.update(id,s);
            if (k)
            {
                return RedirectToAction("IndBlogList");
            }
            else
            {
                return View();
            }




  
        }
        
        public ActionResult DeleteBlog(int id)
        {
            bool k=r.delete(id);
            
                return RedirectToAction("IndBlogList");
          
        }
        public ActionResult DeleteEmployee(string id)
        {
            bool k = rk.delete(id);
            return RedirectToAction("EmployeeList");
        }
        public ActionResult EditEmployee(string id)
        {
            EmpInfo f = rk.Get(id);
            Empl m=new Empl();
            m.Emailid = f.EmailId;
            m.DateOfJoining = f.DateOfJoining;
            m.Passcode = f.Passcode;
            m.Name = f.Name;
            return View(m);
        }
        [HttpPost]
        public ActionResult EditEmployee(string id,FormCollection c)
        {
            EmpInfo emp = new EmpInfo();
            emp.EmailId = c["Emailid"].ToString();
            emp.DateOfJoining = Convert.ToDateTime(c["DateOfJoining"]);
            emp.Name = c["Name"].ToString();
            emp.Passcode = Convert.ToInt32(c["Passcode"]);
            bool k=rk.update(id,emp);
            if (k)
            {
                return RedirectToAction("EmployeeList");
            }
            else
            {
                return View();
            }
        }

    }
}
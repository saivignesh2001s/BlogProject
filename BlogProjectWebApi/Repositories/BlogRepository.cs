using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
namespace BlogProjectWebApi.Repositories
{
    public interface IBlogRepository
    {
        IEnumerable<BlogInfo> GetAll();
        BlogInfo GetById(int id);
        bool insert(BlogInfo m);
        bool update(int id,BlogInfo m);    
        bool delete(int id);
        void Save();
    }
    public class BlogRepository : IBlogRepository
    {
       MyContext context=null;
        public BlogRepository()
        {
           context=new MyContext();

        }

        public bool delete(int id)
        {
            try
            {
                BlogInfo m = context.BlogInfoes.Find(id);
                context.BlogInfoes.Remove(m);
                Save();
                return true;

            }
            catch
            {
                return false;
            }
            
        }

        public IEnumerable<BlogInfo> GetAll()
        {
            return context.BlogInfoes.ToList();
           
        }

        public BlogInfo GetById(int id)
        {
            return context.BlogInfoes.Find(id);
        }

        public bool insert(BlogInfo m)
        {
            try
            {
                context.BlogInfoes.Add(m);
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public bool update(int id,BlogInfo m)
        {
            try
            {
                BlogInfo k = context.BlogInfoes.Find(id);
                k.BlogId = m.BlogId;
                k.BlogUrl = m.BlogUrl;
                k.Title = m.Title;
                k.Subject = m.Subject;
                k.DateOfCreation = m.DateOfCreation;
                k.Emailid = m.Emailid;
                Save();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
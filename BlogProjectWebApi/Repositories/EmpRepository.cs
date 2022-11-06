using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace BlogProjectWebApi.Repositories
{
    public interface IEmpRepository:IDisposable
    {
        IEnumerable<EmpInfo> GetAll();
        EmpInfo Get(string id);
        EmpInfo Verify(string value1,int value2);
        bool insert(EmpInfo empInfo);

        bool delete(string id);
        bool update(string id, EmpInfo emp);
        void Save();
        

    }
    public class EmpRepository : IEmpRepository
    {
        MyContext context = null;
        public EmpRepository()
        {
            context=new MyContext();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public EmpInfo Get(string id)
        {
            var p = context.EmpInfoes.ToList();
            EmpInfo m= p.Find(c=>c.EmailId==id);
            return m;
        }

        public IEnumerable<EmpInfo> GetAll()
        {
            return context.EmpInfoes.ToList();
        }

        public bool insert(EmpInfo empInfo)
        {
            try
            {
                context.EmpInfoes.Add(empInfo);
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

        public EmpInfo  Verify(string value1,int value2)
        {
            EmpInfo f = null;
            foreach(var item in context.EmpInfoes.ToList())
            {
                if (item.EmailId==value1 && item.Passcode==value2)
                {
                    f = item;
                }
            }
            return f;
        }
        public bool delete(string id)
        {
            EmpInfo f = null;
            foreach(var item in context.EmpInfoes.ToList())
            {
                if (item.EmailId == id)
                {
                    f = item;
                }
            }
            if (f!=null)
            {
                context.EmpInfoes.Remove(f);
                Save();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool update(string id,EmpInfo emp)
        {
            try
            {
                EmpInfo k=Get(id);
                k.Name = emp.Name;
                k.Passcode = emp.Passcode;
                k.DateOfJoining = emp.DateOfJoining;
                k.EmailId = emp.EmailId;
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
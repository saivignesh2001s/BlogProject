using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class adminmethods
    {
        MyContext admininfo = null;
        public adminmethods()
        {
            admininfo = new MyContext();
        }
        public bool CheckLogin(AdminInfo m)
        {
            bool k = false;
            foreach(var item in admininfo.AdminInfos.ToList())
            {
               if(item.Emailid==m.Emailid && item.Password == m.Password)
                {
                    k = true;
                }
            }
            return k;
        }
    }
}

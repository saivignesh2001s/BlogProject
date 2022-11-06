using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdminInfo
    {
        [Key]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Emailid
        {
            get;
            set;

        }
        public string Password
        {
            get;
            set;
        }
    }
    public class EmpInfo
    {
        [Key]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public DateTime DateOfJoining
        {
            get;
            set;
        }
        public int Passcode
        {
            get;
            set;
        }

    }
    public class BlogInfo
    {
        [Key]
        [Required]
        public int BlogId
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public string Subject
        {
            get;
            set;
        }
        public DateTime DateOfCreation
        {
            get;
            set;
        }
        public string BlogUrl
        {
            get;
            set;
        }
        public string Emailid
        {
            get;
            set;
        }
      
    }
    public class MyContext : DbContext
    {
        public virtual DbSet<AdminInfo> AdminInfos { get; set; }
        public virtual DbSet<EmpInfo> EmpInfoes { get; set; }
        public virtual DbSet<BlogInfo> BlogInfoes
        {
            get;
            set;
        }
    }
    public class AdminInfoDbInitializer : DropCreateDatabaseIfModelChanges<MyContext> {
        protected override void Seed(MyContext context)
        {
            var adminInfo = new List<AdminInfo>
                {
                    new AdminInfo{Emailid="abc@gmail.com",Password="abc123"},
                     new AdminInfo{Emailid="cba@gmail.com",Password="cba123"}

                };
            adminInfo.ForEach(s => context.AdminInfos.Add(s));
            context.SaveChanges();
        }

    }
}  
    

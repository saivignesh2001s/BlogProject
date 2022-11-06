using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTrackerApp.Models
{
    public class Blog
    {
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
        [DataType(DataType.DateTime)]
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
        [DataType(DataType.EmailAddress)]
        public string Emailid
        {
            get;
            set;
        }

    }
}
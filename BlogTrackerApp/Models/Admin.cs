using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTrackerApp.Models
{
    public class Admin
    {
        [Required]
        [DataType(DataType.EmailAddress)]

        public string EmailId
        {
            get;
            set;
        }
        [DataType(DataType.Password)]
        public string password
        {
            get;
            set;
        }
    }
}
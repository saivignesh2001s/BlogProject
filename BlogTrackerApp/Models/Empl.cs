using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTrackerApp.Models
{
    public class Empl
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Emailid
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
}
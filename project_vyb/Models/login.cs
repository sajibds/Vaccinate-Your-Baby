using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project_vyb.Models
{
    public class login
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "This feild is required")]
        public string username { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "This feild is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
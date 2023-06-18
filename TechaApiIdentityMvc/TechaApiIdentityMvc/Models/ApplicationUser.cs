using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace TechaApiIdentityMvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "FirstName and LastName")]
        public string FullName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "DateOfBirth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
    }
}




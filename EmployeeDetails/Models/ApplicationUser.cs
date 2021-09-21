using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string lastname { get; set; }
        [Required]

        public string Gender { get; set; }
        [Required]

        public string City { get; set; }
        [Required]
        public DateTime DateofBirth { get; set; }




    }
}

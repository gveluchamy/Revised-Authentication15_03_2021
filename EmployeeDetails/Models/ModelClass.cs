using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails.Models
{
    
    
            public class RegisterUserModel
            {
                [Required]
                public string FirstName { get; set; }
                [Required]

                public string lastname { get; set; }
                [Required]

                public string Gender { get; set; }
               
                [Required]
                public DateTime DateofBirth { get; set; }
                [Required]

                public string City { get; set; }




                 [Required]
                [EmailAddress]
                public string Email { get; set; }

                [Required]
                [DataType(DataType.Password)]
                public string Password { get; set; }

                [DataType(DataType.Password)]
                [Display(Name = "Confirm password")]
                [Compare("Password",
                    ErrorMessage = "Password and confirmation password do not match.")]
                public string ConfirmPassword { get; set; }
             }

        public class LoginUserModel
        {

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }

            public string ReturnUrl { get; set; }
            public IList<AuthenticationScheme> ExternalLogins { get; set; }

           
            //public int Id { get; set; }


        }
         
}

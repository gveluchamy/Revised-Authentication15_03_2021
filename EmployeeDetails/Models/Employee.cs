using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace EmployeeDetail.Models
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        [DataType(DataType.Text)]
        //[Display(Name = "Employee Name")]
        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9.]+$", ErrorMessage = "Only Alphabets and NUMBER  allowed but Employee name before number notallowed ")]
        public string Name { get; set; }
        public string Gender { get; set; }

        
        
        [DataType(DataType.Date)]
        //[RegularExpression("^(0[1 - 9] | 1[012])[- /.](0[1 - 9] |[12][0 - 9] | 3[01])[- /.](19 | 20)\d\mm $",ErrorMessage =" THAHJH")]
        public DateTime Dob { get; set; }
        [Required]

        [Display(Name = "Department")]

        public string Department { get; set; }
        
        [Required(ErrorMessage = "Please enter  Email id")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        //[RegularExpression("^(?=.{6,40}@)[a-zA-Z][a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{3,4})$", ErrorMessage = "Invalid email format.")]
        [RegularExpression("^[a-zA-Z][-_.a-zA-Z0-9]{6,50}@g(oogle)?mail.com$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        //[DataType(DataType.Upload)]
        //[Display(Name = " Upload file")]
        ////[RegularExpression("^((([a-zA-Z]:)|(\\{2}+)?)(\\([].*))+(.jpg|.JPG|.jpeg|.JPEG|.bmp|.BMP|.gif|.GIF|.psd|.PSD)$", ErrorMessage = " Valid File Upload")))]
        //[Required(ErrorMessage ="Please choose file to Upload")]
        //public string ImagePath { get; set; }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? RedirectUri { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.



    }

   
}
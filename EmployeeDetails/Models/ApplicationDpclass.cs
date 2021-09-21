using EmployeeDetails.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails.Models
{
    public class ApplicationDpclass:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDpclass(DbContextOptions<ApplicationDpclass> options):base(options)
        {

        }
         //public DbSet<LoginUserModel> AspNetUsers { get; set; } 
    }
}

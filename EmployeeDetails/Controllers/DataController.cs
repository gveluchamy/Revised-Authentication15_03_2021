using EmployeeDetail.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EmployeeDetail.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        EmpDpconnection empDpconnection = new EmpDpconnection();
        List<Employee> employeeL = new List<Employee>();

        public IActionResult Privacy()
        {

            return View();
        }

        [Authorize]
        public IActionResult Index()
        {
            // Employee employeeL = new Employee();



            employeeL = empDpconnection.GetAllEmployeeDetails();


            return View(employeeL);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }



        [HttpPost]
        public IActionResult Create(Employee empA)
        {

            try
            {
                int result = 2;

                empDpconnection.AddEmployees(empA, out result);
                if (ModelState.IsValid)
                {
                    if (result == 1)
                    {
                        ViewBag.get = " Employee Data Save sucessfully !";
                    }
                   


                    employeeL = empDpconnection.GetAllEmployeeDetails();


                    return View("Index", employeeL);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(empA);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {



            return View(empDpconnection.GetAllEmployeeDetails().Where(Emp => Emp.EmpID == id).FirstOrDefault());
        }
        [HttpPost]
        public IActionResult Update(Employee emp)
        {

            try
            {


                int get=5;

                empDpconnection.UpdateEmplyees(emp, out get);
                employeeL = empDpconnection.GetAllEmployeeDetails();

                if (get == 1)
                {
                    ViewBag.get = " Employee Data Update sucessfully !";
                }
                
                


                return View("Index", employeeL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public IActionResult Delete(int id, int EmpID)
        {
           
            try
            {
                int get = 3;
                empDpconnection.DeleteEmployees(id,out get);
                employeeL = empDpconnection.GetAllEmployeeDetails();
                if (get == 1)
                {
                    ViewBag.get = " Employee Data Delete sucessfully !";
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("Index", employeeL);
        }
    }
}

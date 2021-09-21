using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetail.Models
{
    public class EmpDpconnection
    {
        string connetionString = "Data Source = dbms-5; Initial Catalog = ganapathy; User ID = sa; User ID=sa;Password=abc123!@#;";
        
        public List<Employee> GetAllEmployeeDetails()
        {
            List<Employee> employeeL = new List<Employee>();  
            //Employee employeeL = new Employee;

            try
            {
                SqlConnection sqlCon = new SqlConnection(connetionString);
                sqlCon.Open();
                SqlCommand sqlCom = new SqlCommand("GetAllEmployee", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;

                SqlDataReader sqldr = sqlCom.ExecuteReader();

                while (sqldr.Read())
                {
                    Employee remployee = new Employee();
                    remployee.EmpID = Convert.ToInt32(sqldr["EmpID"]);
                    remployee.Name = sqldr["Name"].ToString();
                    remployee.Gender = sqldr["Gender"].ToString();
                    remployee.Dob = Convert.ToDateTime(sqldr["Dob"]);
                    remployee.Department = sqldr["Department"].ToString();
                    remployee.Email = sqldr["Email"].ToString();
                    //remployee.ImagePath = sqldr["ImagePath"].ToString();

                    employeeL.Add(remployee);
                    employeeL.ToLookup(x => x.Department);

                }
                
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return employeeL;
        }
        public void AddEmployees(Employee empA, out int result)
        {

            try
            {

                SqlConnection sqlCon = new SqlConnection(connetionString);
                SqlCommand sqlCom = new SqlCommand("ADDEmployee", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;

                sqlCom.Parameters.AddWithValue("@Name", empA.Name);
                sqlCom.Parameters.AddWithValue("@Gender", empA.Gender);
                sqlCom.Parameters.AddWithValue("@Dob", empA.Dob);
                sqlCom.Parameters.AddWithValue("@Department", empA.Department);
                sqlCom.Parameters.AddWithValue("@Email", empA.Email);
                //sqlCom.Parameters.AddWithValue("@ImagePath", empA.ImagePath);

                sqlCon.Open();
                result = sqlCom.ExecuteNonQuery();

                sqlCon.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateEmplyees(Employee empU, out int get)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connetionString);
                SqlCommand sqlCom = new SqlCommand("UpdateEmployee", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;

                sqlCom.Parameters.AddWithValue("@EmpID", empU.EmpID);
                sqlCom.Parameters.AddWithValue("@Name", empU.Name);
                sqlCom.Parameters.AddWithValue("@Gender", empU.Gender);
                sqlCom.Parameters.AddWithValue("@Dob", empU.Dob);
                sqlCom.Parameters.AddWithValue("@Department", empU.Department);
                sqlCom.Parameters.AddWithValue("@Email", empU.Email);
                //sqlCom.Parameters.AddWithValue("@ImagePath", empU.ImagePath);
                sqlCon.Open();
                get = sqlCom.ExecuteNonQuery();
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteEmployees(int ID, out int get)
        {
            try
            {
                
                SqlConnection sqlcon = new SqlConnection(connetionString);
                SqlCommand sqlCom = new SqlCommand("DeleteEmployee", sqlcon);
                sqlCom.CommandType = CommandType.StoredProcedure;

                sqlCom.Parameters.AddWithValue("@EmpID", ID);
               

                sqlcon.Open();
                get = sqlCom.ExecuteNonQuery();
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using PPM.DAL.Models;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using PPM.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace PPM.DAL
{
    public class EmployeeDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();

        //Get All Employees
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllEmployees";
                MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                DataTable dtEmployees = new DataTable();

                connection.Open();
                sqlDA.Fill(dtEmployees);
                connection.Close();

                foreach (DataRow dr in dtEmployees.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        EmailId = dr["EmailId"].ToString(),
                        MobileNumber = dr["MobileNumber"].ToString(),
                        Address = dr["Address"].ToString(),
                        Password = dr["Password"].ToString(),
                        RoleId = Convert.ToInt32(dr["RoleId"])
                    });
                }
            }

            return employeeList;
        }

        //Insert Employees

        public bool InsertEmployee (Employee employee)
        {
            int id = 0;
      
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command =  new MySqlCommand("sp_InsertEmployees", connection);
                command.CommandType = CommandType.StoredProcedure;
                // command.Parameters.AddWithValue("@p_EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@p_FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@p_LastName", employee.LastName);
                command.Parameters.AddWithValue("@p_EmailId", employee.EmailId);
                command.Parameters.AddWithValue("@p_MobileNumber", employee.MobileNumber);
                command.Parameters.AddWithValue("@p_Password", employee.Password);
                command.Parameters.AddWithValue("@p_Address", employee.Address);
                command.Parameters.AddWithValue("@p_RoleId", employee.RoleId);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
        
        //Get Employees By EmployeeId
        
        public List<Employee> GetEmployeesById(int EmployeeId)
        {
            List<Employee> employeeList = new List<Employee>();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetEmployeeById";
                command.Parameters.AddWithValue("@p_EmployeeId", EmployeeId);
                MySqlDataAdapter sqlDA = new MySqlDataAdapter(command);
                DataTable dtEmployees = new DataTable();

                connection.Open();
                sqlDA.Fill(dtEmployees);
                connection.Close();

                foreach (DataRow dr in dtEmployees.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        EmailId = dr["EmailId"].ToString(),
                        MobileNumber = dr["MobileNumber"].ToString(),
                        Address = dr["Address"].ToString(),
                        Password = dr["Password"].ToString(),
                        RoleId = Convert.ToInt32(dr["RoleId"])
                    });
                }
            }

            return employeeList;
        }

        //Update Employees

        public bool UpdateEmployee (Employee employee)
        {
            int i = 0;
      
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command =  new MySqlCommand("sp_UpdateEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@p_FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@p_LastName", employee.LastName);
                command.Parameters.AddWithValue("@p_EmailId", employee.EmailId);
                command.Parameters.AddWithValue("@p_MobileNumber", employee.MobileNumber);
                command.Parameters.AddWithValue("@p_Password", employee.Password);
                command.Parameters.AddWithValue("@p_Address", employee.Address);
                command.Parameters.AddWithValue("@p_RoleId", employee.RoleId);

                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }
            

        //Delete Employee By EmployeeId
        public string DeleteEmployee(int employeeId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand command = new MySqlCommand("sp_DeleteEmployee", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_EmployeeId", employeeId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                
                    if (rowsAffected > 0)
                    {
                        return "Employee Deleted Successfully";
                    }
                    else
                    {
                        return "Deletion failed. Employee Not Found.";
                    }  
                }
                catch (Exception ex)
                {
                    return "Error Deleting Employee: " + ex.Message;
                } 
                finally
                {
                    connection.Close();
                }     
            }
        }
    }
    
}

        
            
                    
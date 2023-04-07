using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPM.DAL.Models;
using PPM.DAL.Data;
using PPM.DAL;


namespace PPM.UI.WEB.Controllers;

public class EmployeeController : Controller
{
    // private readonly ILogger<EmployeeController> _logger;

    // public EmployeeController(ILogger<EmployeeController> logger)
    // {
    //     _logger = logger;
    // }

        EmployeeDAL employeeDAL =  new EmployeeDAL();
    
        //GET : EMPLOYEE
        
        [HttpGet]
        public IActionResult Index()
        {
            var employeeList = employeeDAL.GetAllEmployees();
            if (employeeList.Count == 0)
            {
                TempData ["InfoMessage"] = "Currently Employees Not Avaiable in the DataBase...";
            }
            return View(employeeList);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }

        //POST : Employee/Create
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = employeeDAL.InsertEmployee(employee);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Employee Details Saved Successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Save the Employee Details...";
                    }
                }
                return RedirectToAction("Index");
            }
             catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

            // return a default view if the ModelState is not valid
            return View();
        }

        //GET : EMPLOYEE Edit
        public IActionResult Edit(int id)
        {
            try
            {
                var employee = employeeDAL.GetEmployeesById(id).FirstOrDefault();
                if (employee == null)
                {
                    TempData["InfoMessage"] = "Employee Not Available With Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                    return View(employee);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        //POST : Employee/Edit
        [HttpPost , ActionName("Edit")]
        public IActionResult UpdateEmployee (Employee employee)
        {
            // bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    bool IsInserted = employeeDAL.UpdateEmployee(employee);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Employee Details Updated Successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Update the Employee Details...";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

            // return a default view if the ModelState is not valid
            return View();
        }

        //GET : Employee Delete
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = employeeDAL.GetEmployeesById(id).FirstOrDefault();
                if (employee == null)
                {
                    TempData["InfoMessage"] = "Employee Not Available With Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        //POST : Employee/Delete
        [HttpPost , ActionName("Delete")]

        public IActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = employeeDAL.DeleteEmployee(id); 

                if (result.Contains("Employee Deleted Successfully"))
                {
                    TempData["SuccessMessage"] = result ;
                }
                else
                {
                    TempData["ErrorMessage"] = result ;
                }
                return RedirectToAction("Index");  
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the employee: " + ex.Message;
                return View();
            }
        }

        //GET : Employee Details
        public IActionResult Details(int id)
        {
            try
            {
                var employee = employeeDAL.GetEmployeesById(id).FirstOrDefault();
                if (employee == null)
                {
                    TempData["InfoMessage"] = "Employee Not Available With Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        
    }


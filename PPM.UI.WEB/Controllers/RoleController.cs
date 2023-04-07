using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPM.DAL.Models;
using PPM.DAL.Data;
using PPM.DAL;


namespace PPM.UI.WEB.Controllers;

public class RoleController : Controller
{
    // private readonly ILogger<RoleController> _logger;

    // public RoleController(ILogger<RoleController> logger)
    // {
    //     _logger = logger;
    // }

        RoleDAL roleDAL =  new RoleDAL();
    
        //GET : Role
        
        [HttpGet]
        public IActionResult Index()
        {
            var roleList = roleDAL.GetAllRoles();
            if (roleList.Count == 0)
            {
                TempData ["InfoMessage"] = "Currently Roles Not Avaiable in the DataBase...";
            }
            return View(roleList);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }

        //POST : Role/Create
        [HttpPost]
        public IActionResult Create(Role Role)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = roleDAL.InsertRole(Role);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Role Details Saved Successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Save the Role Details...";
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

        //GET : Role Edit
        public IActionResult Edit(int id)
        {
            var role = roleDAL.GetRolesById(id).FirstOrDefault();
                if (role == null)
                {
                    TempData["InfoMessage"] = "Role Not Available With Id" + id.ToString();
                    return RedirectToAction("Index");
                }
            return View(role);
            }


            

        //POST : Role/Edit
        [HttpPost , ActionName("Edit")]
        public IActionResult UpdateRole (Role role)
        {
            // bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    bool IsInserted = roleDAL.UpdateRole(role);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Role Details Updated Successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Update the Role Details...";
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

        //GET : Role Delete
        public IActionResult Delete(int id)
        {
            try
            {
                var role = roleDAL.GetRolesById(id).FirstOrDefault();
                if (role == null)
                {
                    TempData["InfoMessage"] = "Role Not Available With Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(role);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        

        // POST: Role/Delete
        [HttpPost , ActionName("Delete")]
        public IActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = roleDAL.DeleteRole(id);

                if (result.Contains("Role Deleted Successfully"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the employee: " + ex.Message;
                return View();
            }
        }

        //GET : Role Details
        public IActionResult Details(int id)
        {
            try
            {
                var role = roleDAL.GetRolesById(id).FirstOrDefault();
                if (role == null)
                {
                    TempData["InfoMessage"] = "Role Not Available With Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(role);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

    }


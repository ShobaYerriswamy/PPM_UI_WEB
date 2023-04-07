using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPM.DAL.Models;
using PPM.DAL.Data;
using PPM.DAL;


namespace PPM.UI.WEB.Controllers;

public class ProjectController : Controller
{
    // private readonly ILogger<ProjectController> _logger;

    // public ProjectController(ILogger<ProjectController> logger)
    // {
    //     _logger = logger;
    // }

        ProjectDAL projectDAL =  new ProjectDAL();
    
        //GET : Project
        
        [HttpGet]
        public IActionResult Index()
        {
            var projectList = projectDAL.GetAllProjects();
            if (projectList.Count == 0)
            {
                TempData ["InfoMessage"] = "Currently Projects Not Avaiable in the DataBase...";
            }
            return View(projectList);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }

        //POST : Project/Create
        [HttpPost]
        public IActionResult Create(Project project)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = projectDAL.InsertProject(project);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Project Details Saved Successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Save the Project Details...";
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

        //GET : Project Edit
        public IActionResult Edit(int id)
        {
            var project = projectDAL.GetProjectsById(id).FirstOrDefault();
                if (project == null)
                {
                    TempData["InfoMessage"] = "Project Not Available With Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(project);
        }

        //POST : Project/Edit
        [HttpPost , ActionName("Edit")]
        public IActionResult UpdateProject (Project project)
        {
            // bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    bool IsInserted = projectDAL.UpdateProject(project);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Project Details Updated Successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Update the Project Details...";
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

        //GET : Project Delete
        public IActionResult Delete(int id)
        {
            try
            {
                var project = projectDAL.GetProjectsById(id).FirstOrDefault();
                if (project == null)
                {
                    TempData["InfoMessage"] = "Project Not Available With Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(project);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

    
        // POST: Project/Delete
        [HttpPost , ActionName("Delete")]
        public IActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = projectDAL.DeleteProject(id);

                if (result.Contains("Project Deleted Successfully"))
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

        //GET : Project Details
        public IActionResult Details(int id)
        {
            try
            {
                var project = projectDAL.GetProjectsById(id).FirstOrDefault();
                if (project == null)
                {
                    TempData["InfoMessage"] = "Project Not Available With Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(project);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

    }


using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPM.DAL.Models;
using PPM.DAL.Data;
using PPM.DAL;


namespace PPM.UI.WEB.Controllers;

public class LoginController : Controller
{
    // private readonly ILogger<ProjectController> _logger;

    // public ProjectController(ILogger<ProjectController> logger)
    // {
    //     _logger = logger;
    // }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    
    // [HttpPost]
    // public IActionResult Login(LoginViewModel model)
    // {
    //     // Validate the login information and perform the login logic
    //     // ...
        
    //     return RedirectToAction("Index", "Employee");
    // }

}

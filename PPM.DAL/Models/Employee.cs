using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPM.DAL.Models;

public partial class Employee
{
    [Required(ErrorMessage = "EmployeeId is required")]
    public int EmployeeId { get; set; }
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;
    [Required(ErrorMessage = "EmailId  is required")]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email format")]
    public string EmailId { get; set; } = null!;
    [Required(ErrorMessage = "MobileNumber is required")]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Mobile number should be 10 digits.")]
    public string MobileNumber { get; set; } = null!;
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Address is required")]
    public string? Address { get; set; }
    [Required(ErrorMessage = "RoleId is required")]
    public int RoleId { get; set; }

    public virtual ICollection<Project> Projects { get; } = new List<Project>();
}

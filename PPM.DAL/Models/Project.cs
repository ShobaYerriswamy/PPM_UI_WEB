using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPM.DAL.Models;

public partial class Project
{
    [Required(ErrorMessage = "ProjectId is required")]
    public int ProjectId { get; set; }
    [Required(ErrorMessage = "ProjectName is required")]
    public string ProjectName { get; set; } = null!;
    [Required(ErrorMessage = "StartDate is required")]
    [RegularExpression(@"^(0[1-9]|[1-2][0-9]|3[0-1])/(0[1-9]|1[0-2])/[0-9]{4}$", ErrorMessage = "Invalid date format. Use dd/mm/yyyy.")]
    public string StartDate { get; set; } = null!;
    [Required(ErrorMessage = "EndDate is required")]
    [RegularExpression(@"^(0[1-9]|[1-2][0-9]|3[0-1])/(0[1-9]|1[0-2])/[0-9]{4}$", ErrorMessage = "Invalid date format. Use dd/mm/yyyy.")]
    public string EndDate { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}

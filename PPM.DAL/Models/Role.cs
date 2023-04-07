using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPM.DAL.Models;

public partial class Role
{
    [Required(ErrorMessage = "RoleId is required")]
    public int RoleId { get; set; }
    [Required(ErrorMessage = "RoleName is required")]
    public string? RoleName { get; set; }
}

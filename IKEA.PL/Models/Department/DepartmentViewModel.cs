﻿using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.Models.Department
{
    public class DepartmentViewModel
    {
        [Required(ErrorMessage ="Code Is Required !!")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}

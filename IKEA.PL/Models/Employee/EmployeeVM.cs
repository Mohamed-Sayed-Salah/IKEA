﻿using IKEA.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.Models.Employee
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
        [MinLength(3, ErrorMessage = "Min Length of Name is 3 Chars")]

        public string Name { get; set; } = null!;
        [Range(22, 30)]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{4,10}-[a-zA-Z]{4,10}-[a-zA-Z]{4,10}$"
                       , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        public decimal? Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; } = null!;
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}

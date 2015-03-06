using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSBOX.Data.ViewModels
{
    public class EmployeeRegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }


        
        [StringLength(150)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(150)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Salary Type")]
        [StringLength(50)]
        public string Salary_Type { get; set; }

        [Display(Name = "Manager ID")]
        public int? Manager_ID { get; set; }

        [StringLength(20)]
        public string Title { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime? Hire_Date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Salary { get; set; }

        [Display(Name = "Vacation Hours")]
        [Column(TypeName = "numeric")]
        public decimal? Vacation_Hrs { get; set; }

        [Display(Name = "Sick leave hours")]
        [Column(TypeName = "numeric")]
        public decimal? SickLeave_Hrs { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? Created_Date { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? Modified_Date { get; set; }
    }
}
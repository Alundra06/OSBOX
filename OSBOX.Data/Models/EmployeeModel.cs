using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSBOX.Data.Models
{
    [Table("Employee")]
    public class EmployeeModel 
    {
        public EmployeeModel()
        {
            //Tasks = new List<TaskModel>();
        }

       //TODO used for AK2CPA
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int Employee_ID { get; set; }
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
         [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
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
         [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Modified_Date { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        public List<TaskModel> Tasks { get; set; }

    }


   
    
}
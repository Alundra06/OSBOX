using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OSBOX.Data.Models
{
    [Table("Task")]
    public class TaskModel
    {
        
        
        
        [Key]
        public string TasksID { get; set; }
        [Display(Name = "Title")]

        //[StringLength(128, ErrorMessage = "Must be at least {3} characters long.", MinimumLength = 3)]
        public string Name { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Planned due date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime DueDate { get; set; }

        [Display(Name = "Complete %")]
        public Nullable<int> Complete { get; set; }

        public string Description { get; set; }

        public Nullable<int> Priority { get; set; }

        [Display(Name = "Task Status")]
        public string Status { get; set; }

        [Display(Name = "Assignment date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime? AssignementDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Actual completion date")]
        public DateTime? CompletionDate { get; set; }
        
        
        [Display(Name = "Assigned to")]
        
        public  Nullable<int> Employee_ID  { get; set; }

        
        //Optional field for different purposes
        public Nullable<Boolean> option { get; set; }

        public virtual EmployeeModel Employee { get; set; }

        [Display(Name = "Customer")]
        public Nullable<int> CustomerID { get; set; }

        public virtual CustomerModel Customer { get; set; }

        //public int FileID { get; set; }
        public List<FileModel> Files { get; set; }



        
        
    }
}
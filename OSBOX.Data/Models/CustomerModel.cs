using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OSBOX.Data.Models
{
    [Table("Customer")]
    public class CustomerModel
    {
       
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
      
        public int CustomerId { get; set; }

         [Required]
         [StringLength(128)]
         public string UserId { get; set; }

         [StringLength(10)]
         [Display(Name = "Name Prefix")]
         public string Name_Prefix { get; set; }

         [StringLength(50)]
         [Display(Name = "Last Name")]
         public string Last_Name { get; set; }

         [StringLength(50)]
         [Display(Name = "Middle Name")]
         public string Middle_name { get; set; }

         [StringLength(50)]
         [Display(Name = "First Name")]
         public string First_Name { get; set; }

         [StringLength(255)]
         [Display(Name = "Business Name")]
         public string Business_Name { get; set; }

         [StringLength(255)]
         [Display(Name = "Legal Name")]
         public string LegalName { get; set; }

         [StringLength(15)]
         [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number! (Please use the format (xxx-xxx-xxxx)")]
         [Display(Name = "Phone Number")]
         public string Phone_Number { get; set; }

         [StringLength(15)]
         [Display(Name = "Cell Phone")]
         [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Cell Phone Number!(Please use the format (xxx-xxx-xxxx)")]
         public string Cell_Phone { get; set; }

         [StringLength(15)]
         [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Fax Number!(Please use the format (xxx-xxx-xxxx)")]
         public string Fax { get; set; }

         [StringLength(50)]
         [EmailAddress(ErrorMessage = "Invalid Email Address")]
         public string Email { get; set; }

         [StringLength(255)]
         [Display(Name = "ID Code")]
         public string ID_Code { get; set; }
        [Display(Name = "Date Started")]
         public Nullable<DateTime> DateStarted { get; set; }
        [Display(Name = "Date Ended")]
         public Nullable<DateTime> DateEnded { get; set; }
         [StringLength(15)]
         [Display(Name = "Billing Type")]
        public String BillingType { get; set; }


        public List<TaskModel> Tasks { get; set; }
      
        public virtual ICollection<BusinessModel> Businesses { get; set; }
        public virtual ICollection<InvoiceModel> Invoices { get; set; }
        public virtual ICollection<AddressModel> Addresses { get; set; }
     
  
   
        
    }
}
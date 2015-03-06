using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//using System.Web.Mvc;

namespace OSBOX.Data.ViewModels
{
    public class CustomerRegisterViewModel
    {

        
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is a mandatory Field!")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Business Name")]
        public string Business_Name { get; set; }

        [StringLength(10)]
        [Display(Name = "Name Prefix")]
        public string Name_Prefix { get; set; }


        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string Middle_name { get; set; }

        

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

        [Required(ErrorMessage="ID Code is a mandatory Field!")]
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


        //The address POCO

        [Display(Name = "Street address 1")]
        public string Street_Adr1 { get; set; }
        [Display(Name = "Street address 2")]
        public string Street_Adr2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name = "Address type")]
        [StringLength(1, ErrorMessage = "Please type H or B")]
        public string Addresstype { get; set; }




        public  List<System.Web.Mvc.SelectListItem> States { get; set; }
       

    //    public static List<System.Web.Mvc.System.Web.Mvc.SelectListItem> States = new List<System.Web.Mvc.System.Web.Mvc.SelectListItem>()
    //{
        
    //};
       
     
    }
}
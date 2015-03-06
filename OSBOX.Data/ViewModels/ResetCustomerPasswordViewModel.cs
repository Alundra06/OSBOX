using System;
using System.ComponentModel.DataAnnotations;

namespace OSBOX.Data.ViewModels
{
    public class ResetCustomerPasswordViewModel
    {
        
        
        
        [Required(ErrorMessage= "Please provide a password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = " New password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public int CustomerID { get; set; }//Just to hold the value of the Customer ID
        
       
     
    }
}
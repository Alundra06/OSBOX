
using System;
using System.ComponentModel.DataAnnotations;

namespace OSBOX.Data.ViewModels
{
    public class CreatePaymentViewModel
    {

        public int Payment_ID { get; set; }
        public int Invoice_ID { get; set; }
        public int CustomerId { get; set; }
        public Nullable<int> Payment_Type_ID { get; set; }



        [StringLength(255)]
        [Display(Name = "Business Name")]
        public string Business_Name { get; set; }


        [StringLength(255)]
        [Display(Name = "ID Code")]
        public string ID_Code { get; set; }

        [Display(Name = "Invoice Number")]
        public string Invoice_Number { get; set; }

      
        [Display(Name = "Invoice Amount")]

        public Nullable<decimal> Invoice_Amount { get; set; }

        
        [Display(Name = "Due amount")]
        public Nullable<decimal> Due_Amount { get; set; }


         [Required]
        [Display(Name = "Payment Date")]
        public Nullable<System.DateTime> PaymentDate { get; set; }
         [Required]
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        [Display(Name = "Received by")]
        public string receivedBy { get; set; }

       
     
    }
}
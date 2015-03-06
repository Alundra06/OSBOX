using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSBOX.Data.Models
{
     [Table("Invoice")]
    public class InvoiceModel
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Invoice_ID { get; set; }
         [Required]
         [Display(Name = "Invoice Number")]
         public string Invoice_Number { get; set; }
        [Display(Name = "Invoice Name")]
         public string Invoice_Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Invoice Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Required]
         public Nullable<System.DateTime> Invoice_Date { get; set; }
        [Display(Name = "Invoice Amount")]

        [Required]
         public Nullable<decimal> Invoice_Amount { get; set; }
        [Display(Name = "Due amount")]

        [Required]
         public Nullable<decimal> Due_Amount { get; set; }
        [Display(Name = "Due date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
         public Nullable<System.DateTime> DueDate { get; set; }
        [Display(Name = "Paid status")]
         public string Paid_Status { get; set; }
        [Display(Name = "Payment term")]
         public string Payment_Term { get; set; }
        
        public string Note { get; set; }

        public int CustomerId { get; set; }
        public string ServiceTypeID { get; set; }
        public virtual CustomerModel Customer { get; set; }
        public virtual ServiceTypeModel ServiceType { get; set; }
        public virtual ICollection<PaymentModel> Payments { get; set; }


     
    }
}

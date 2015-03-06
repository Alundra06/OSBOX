using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSBOX.Data.Models
{
    [Table("Payment")]
    public class PaymentModel
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Payment_ID { get; set; }
        public int Invoice_ID { get; set; }
        public Nullable<int> Payment_Type_ID { get; set; }
          [Required]
        [Display(Name = "Payment Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> PaymentDate { get; set; }
         [Required]
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        [Display(Name = "Received by")]
        public string receivedBy { get; set; }

        public virtual InvoiceModel Invoice { get; set; }
        public virtual PaymentTypeModel Payment_Type { get; set; }
        public virtual ICollection<PaymentHistoryModel> Payment_History { get; set; }
    }
}

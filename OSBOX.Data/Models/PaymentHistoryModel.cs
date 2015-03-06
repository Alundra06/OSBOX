using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSBOX.Data.Models
{
     [Table("PaymentHistory")]
    public class PaymentHistoryModel
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Payment_History_ID { get; set; }
        public int Payment_ID { get; set; }
        [Display(Name = "Created date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
         public Nullable<System.DateTime> Created_Date { get; set; }

        public virtual PaymentModel Payment { get; set; }
    }
}

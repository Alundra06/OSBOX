using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSBOX.Data.Models
{
    [Table("Payment_Type")]
    public class PaymentTypeModel
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Payment_Type_ID { get; set; }
        [Display(Name = "Payment Type")]
        public string Payment_Name { get; set; }

        public virtual ICollection<PaymentModel> Payments { get; set; }
    }
}

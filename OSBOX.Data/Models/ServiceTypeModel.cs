

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSBOX.Data.Models
{
    [Table("ServiceType")]
    public class ServiceTypeModel
    {
        [Key]
        public string ID { get; set; }
        
        [Required]
        [Display(Name = "Service Type Name")]
        public string ServiceTypeName { get; set; }

        public virtual ICollection<InvoiceModel> Invoices { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSBOX.Data.Models
{
    [Table("Address")]
    public class AddressModel
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

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

        public int CustomerId { get; set; }

        public virtual CustomerModel Customer { get; set; }


        //To hold the states
        //public List<System.Web.Mvc.SelectListItem> States { get; set; }

    }
}

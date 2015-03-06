using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OSBOX.Data.Models
{
   

    [Table("Business")]
    public  class BusinessModel
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BusinessID { get; set; }
        
        [Display(Name = "Business Name")]
        public string Description { get; set; }
         
        [Display(Name = "Fin number")]
        public string FIN { get; set; }
        public string CR { get; set; }
        public string SUTA { get; set; }
        public string Entity { get; set; }
        public string Filing { get; set; }
        public string BFile { get; set; }
        public string Status { get; set; }
        public string Info { get; set; }
        public string Div { get; set; }
        public string EFTPS { get; set; }
        
      
        public int CustomerId { get; set; }

        public virtual CustomerModel Customer { get; set; }
       
    }
}

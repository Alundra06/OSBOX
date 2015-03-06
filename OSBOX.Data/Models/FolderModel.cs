using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSBOX.Data.Models
{
    [Table("Folder")]
    public class FolderModel
    {
        [Key]
        public string FolderID { get; set; }

        [Display(Name = "Folder Name")]
        public string Name { get; set; }

        [Display(Name = "Creation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Parent Folder")]
        public String ParentFolder { get; set; }

        
        //To store the full address path of a folder
        [Display(Name = "Full Path")]
        public String FullPath { get; set; }

        //To store the type of the folder , mainly to identify System folders that should not be deleted
        [Display(Name = "Type")]
        public String Type { get; set; }





        


        

    }
}
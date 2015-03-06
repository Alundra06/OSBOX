using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSBOX.Data.Models
{
    [Table ("File")]
    public class FileModel
    {
        [Key]
        public string FileID { get; set; }

        [Display(Name = "File Name")]
        public string Name { get; set; }

        [Display(Name = "Upload Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }

        [Display(Name = "File Path")]
        public String FilePath { get; set; }

        [Display(Name = "Folder Path")]
        public String FolderPath { get; set; }
        
        //TO hold the id of the user uploading the file
        //[StringLength(128)]
        //public string UserId { get; set; }

        [StringLength(128)]
        public string UserIdd { get; set; }


       




       //Foreign key for the folder model
        //Every file should belong to a folder
        public string FolderID { get; set; }
       
        public virtual FolderModel Folder { get; set; }





        //[Display(Name = "Customer")]
        //public Nullable<int> CustomerID { get; set; }
      

        [Display(Name = "Task")]
        [StringLength(128)]
        public String TasksID { get; set; }

        //public virtual CustomerModel Customer { get; set; }
        public virtual TaskModel Task { get; set; }

    }
}
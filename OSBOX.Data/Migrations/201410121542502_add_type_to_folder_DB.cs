namespace OSBOX.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class add_type_to_folder_DB : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Folder",
            //    c => new
            //        {
            //            FolderID = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(),
            //            CreationDate = c.DateTime(nullable: false),
            //            ParentFolder = c.String(),
            //            FullPath = c.String(),
            //            Type = c.String(),
            //        })
            //    .PrimaryKey(t => t.FolderID);
            //AddColumn("dbo.Folder", "Type", c => c.String()); 
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.Folder");
        }
    }
}

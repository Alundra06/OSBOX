namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changebusinessattributes3 : DbMigration
    {
        public override void Up()
        {
            //RenameColumn("dbo.Business", "FIN#", "FIN");
            //RenameColumn("dbo.Business", "CR#", "CR");
            //RenameColumn("dbo.Business", "SUTA#", "SUTA");
            //RenameColumn("dbo.Business", "B-File", "BFile");
        }
        
        public override void Down()
        {
        }
    }
}

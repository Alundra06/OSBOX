namespace AKCPA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifyservicetype3InvoiceViasoftech : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.ServiceType", "ID", c => c.String(nullable: false, maxLength: 128));
            //RenameColumn(table: "dbo.Invoice", name: "ServiceType_ID", newName: "ServiceTypeID");
        }
        
        public override void Down()
        {
        }
    }
}

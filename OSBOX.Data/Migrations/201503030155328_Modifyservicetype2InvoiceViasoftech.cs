namespace AKCPA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifyservicetype2InvoiceViasoftech : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Invoice", "ServiceType_ServiceTypeID", "dbo.ServiceType");
            //DropIndex("dbo.Invoice", new[] { "ServiceType_ServiceTypeID" });
            //RenameColumn(table: "dbo.Invoice", name: "ServiceType_ServiceTypeID", newName: "ServiceType_ID");
            //AddColumn("dbo.ServiceType", "ID", c => c.String(nullable: false, maxLength: 128));
            //DropPrimaryKey("dbo.ServiceType");
            //AddPrimaryKey("dbo.ServiceType", "ID");
            //CreateIndex("dbo.Invoice", "ServiceType_ID");
            //AddForeignKey("dbo.Invoice", "ServiceType_ID", "dbo.ServiceType", "ID");
            //DropColumn("dbo.ServiceType", "ServiceTypeID");
        }
        
        public override void Down()
        {
        }
    }
}

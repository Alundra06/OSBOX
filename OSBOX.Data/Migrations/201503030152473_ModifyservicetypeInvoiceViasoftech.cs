namespace AKCPA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyservicetypeInvoiceViasoftech : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.ServiceType",
            //    c => new
            //        {
            //            ServiceTypeID = c.String(nullable: false, maxLength: 128),
            //            ServiceTypeName = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ServiceTypeID);

            //AddColumn("dbo.Invoice", "ServiceType_ServiceTypeID", c => c.String(maxLength: 128));
            //CreateIndex("dbo.Invoice", "ServiceType_ServiceTypeID");
            //AddForeignKey("dbo.Invoice", "ServiceType_ServiceTypeID", "dbo.ServiceType", "ServiceTypeID");
        }
        
        public override void Down()
        {
        }
    }
}

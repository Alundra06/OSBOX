namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servicetype2 : DbMigration
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
            //AddColumn("dbo.ServiceType", "ServiceTypeID", c => c.String(nullable: false, maxLength: 128));
            //DropForeignKey("dbo.Invoice", "ServiceType_ID", "dbo.ServiceType");
            //DropIndex("dbo.Invoice", new[] { "ServiceType_ID" });
            //DropPrimaryKey("dbo.ServiceType");
            //AddPrimaryKey("dbo.ServiceType", "ServiceTypeID");
            //DropColumn("dbo.ServiceType", "ID");
            //RenameColumn(table: "dbo.Invoice", name: "ServiceType_ID", newName: "ServiceType_ServiceTypeID");
            //CreateIndex("dbo.Invoice", "ServiceType_ServiceTypeID");
            //AddForeignKey("dbo.Invoice", "ServiceType_ServiceTypeID", "dbo.ServiceType", "ServiceTypeID");
        }
    }
}

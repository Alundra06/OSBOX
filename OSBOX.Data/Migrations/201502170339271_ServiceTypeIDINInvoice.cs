namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceTypeIDINInvoice : DbMigration
    {
        public override void Up()
        {
            //RenameColumn(table: "dbo.Invoice", name: "ServiceType_ID", newName: "ServiceTypeID");
        }
        
        public override void Down()
        {
            //RenameColumn(table: "dbo.Invoice", name: "ServiceTypeID", newName: "ServiceType_ID");
        }
    }
}

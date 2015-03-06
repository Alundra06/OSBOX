namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeError1 : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.InvoiceModels", newName: "Invoice");
        }
        
        public override void Down()
        {
            //RenameTable(name: "dbo.Invoice", newName: "InvoiceModels");
        }
    }
}

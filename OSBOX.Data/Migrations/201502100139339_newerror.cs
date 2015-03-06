namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newerror : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Invoice", "Invoice_Date", c => c.DateTime());
            //AlterColumn("dbo.Invoice", "Invoice_Amount", c => c.Decimal(precision: 18, scale: 2));
            //AlterColumn("dbo.Invoice", "Due_Amount", c => c.Decimal(precision: 18, scale: 2));
            //AlterColumn("dbo.Payment", "PaymentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.Payment", "PaymentDate", c => c.DateTime(nullable: false));
            //AlterColumn("dbo.Invoice", "Due_Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            //AlterColumn("dbo.Invoice", "Invoice_Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            //AlterColumn("dbo.Invoice", "Invoice_Date", c => c.DateTime(nullable: false));
        }
    }
}

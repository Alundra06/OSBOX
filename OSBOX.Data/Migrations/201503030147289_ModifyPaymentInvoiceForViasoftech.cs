namespace AKCPA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyPaymentInvoiceForViasoftech : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Invoice", "Invoice_Date", c => c.DateTime(nullable: false));
            //AlterColumn("dbo.Invoice", "Invoice_Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            //AlterColumn("dbo.Invoice", "Due_Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            ////AlterColumn("dbo.Payment", "PaymentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
        }
    }
}

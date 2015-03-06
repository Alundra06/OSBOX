namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ID_CODE_Customer : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Customer", "ID_Code", c => c.String(maxLength: 255));
            //DropColumn("dbo.Customer", "Customer_Account");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Customer", "Customer_Account", c => c.String(maxLength: 255));
            //DropColumn("dbo.Customer", "ID_Code");
        }
    }
}

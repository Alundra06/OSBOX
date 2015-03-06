namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeechange : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Employee", "Employee_ID", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.Employee", "Employee_ID", c => c.Int(nullable: false));
        }
    }
}

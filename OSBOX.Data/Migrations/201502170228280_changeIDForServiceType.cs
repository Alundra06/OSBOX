namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIDForServiceType : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.ServiceType", "ID", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.ServiceType", "ID", c => c.String(nullable: false, maxLength: 128));
        }
    }
}

namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixemployeeissue : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Business",
            //    c => new
            //        {
            //            BusinessID = c.Int(nullable: false, identity: true),
            //            Description = c.String(),
            //            FIN = c.String(),
            //            CR = c.String(),
            //            SUTA = c.String(),
            //            Entity = c.String(),
            //            Filing = c.String(),
            //            BFile = c.String(),
            //            Status = c.String(),
            //            Info = c.String(),
            //            Div = c.String(),
            //            EFTPS = c.String(),
            //            CustomerId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.BusinessID)
            //    .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
            //    .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Business", "CustomerId", "dbo.Customer");
            //DropIndex("dbo.Business", new[] { "CustomerId" });
            //DropTable("dbo.Business");
        }
    }
}

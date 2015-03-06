namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressPOCO : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Address",
            //    c => new
            //        {
            //            AddressId = c.Int(nullable: false, identity: true),
            //            Street_Adr1 = c.String(),
            //            Street_Adr2 = c.String(),
            //            City = c.String(),
            //            State = c.String(),
            //            ZipCode = c.String(),
            //            Addresstype = c.String(),
            //            CustomerId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.AddressId)
            //    .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
            //    .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Address", "CustomerId", "dbo.Customer");
            //DropIndex("dbo.Address", new[] { "CustomerId" });
            //DropTable("dbo.Address");
        }
    }
}

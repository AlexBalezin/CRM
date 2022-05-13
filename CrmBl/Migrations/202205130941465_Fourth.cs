namespace CrmBl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Carts", new[] { "Customer_Id" });
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Carts", "Customer_Id");
            AddForeignKey("dbo.Carts", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}

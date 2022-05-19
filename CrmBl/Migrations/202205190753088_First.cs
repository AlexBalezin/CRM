namespace CrmBl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Email = c.String(),
                        Pass = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Checks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Count = c.Int(nullable: false),
                        Seller_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sellers", t => t.Seller_Id)
                .Index(t => t.Seller_Id);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sells",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Check_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Checks", t => t.Check_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Check_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sells", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Sells", "Check_Id", "dbo.Checks");
            DropForeignKey("dbo.Products", "Seller_Id", "dbo.Sellers");
            DropForeignKey("dbo.Checks", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Carts", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "User_Id", "dbo.Users");
            DropIndex("dbo.Sells", new[] { "Product_Id" });
            DropIndex("dbo.Sells", new[] { "Check_Id" });
            DropIndex("dbo.Products", new[] { "Seller_Id" });
            DropIndex("dbo.Checks", new[] { "Customer_Id" });
            DropIndex("dbo.Customers", new[] { "User_Id" });
            DropIndex("dbo.Carts", new[] { "Customer_Id" });
            DropTable("dbo.Sells");
            DropTable("dbo.Sellers");
            DropTable("dbo.Products");
            DropTable("dbo.Checks");
            DropTable("dbo.Users");
            DropTable("dbo.Customers");
            DropTable("dbo.Carts");
        }
    }
}

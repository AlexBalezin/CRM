namespace CrmBl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Seller_Id", c => c.Int());
            CreateIndex("dbo.Products", "Seller_Id");
            AddForeignKey("dbo.Products", "Seller_Id", "dbo.Sellers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Seller_Id", "dbo.Sellers");
            DropIndex("dbo.Products", new[] { "Seller_Id" });
            DropColumn("dbo.Products", "Seller_Id");
        }
    }
}

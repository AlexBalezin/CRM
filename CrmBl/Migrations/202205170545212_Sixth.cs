namespace CrmBl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sixth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Check_Id", "dbo.Checks");
            DropIndex("dbo.Products", new[] { "Check_Id" });
            DropColumn("dbo.Products", "Check_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Check_Id", c => c.Int());
            CreateIndex("dbo.Products", "Check_Id");
            AddForeignKey("dbo.Products", "Check_Id", "dbo.Checks", "Id");
        }
    }
}

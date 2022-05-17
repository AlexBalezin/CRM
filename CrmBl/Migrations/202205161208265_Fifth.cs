namespace CrmBl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fifth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checks", "Sum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Check_Id", c => c.Int());
            CreateIndex("dbo.Products", "Check_Id");
            AddForeignKey("dbo.Products", "Check_Id", "dbo.Checks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Check_Id", "dbo.Checks");
            DropIndex("dbo.Products", new[] { "Check_Id" });
            DropColumn("dbo.Products", "Check_Id");
            DropColumn("dbo.Checks", "Sum");
        }
    }
}

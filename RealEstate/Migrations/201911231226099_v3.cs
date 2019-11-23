namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agreement", "Property_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Agreement", "Property_Id");
            AddForeignKey("dbo.Agreement", "Property_Id", "dbo.Property", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agreement", "Property_Id", "dbo.Property");
            DropIndex("dbo.Agreement", new[] { "Property_Id" });
            DropColumn("dbo.Agreement", "Property_Id");
        }
    }
}

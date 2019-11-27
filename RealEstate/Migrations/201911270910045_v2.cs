namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Property", "image1", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Property", "image1", c => c.Binary(nullable: false));
        }
    }
}

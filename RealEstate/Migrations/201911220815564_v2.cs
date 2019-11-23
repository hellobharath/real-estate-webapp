namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Property", "City", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Property", "City");
        }
    }
}

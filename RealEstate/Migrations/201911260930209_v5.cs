namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Residential", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Residential", "Location", c => c.String(maxLength: 50));
        }
    }
}

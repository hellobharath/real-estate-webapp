namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Plot", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plot", "Location", c => c.String(maxLength: 50));
        }
    }
}

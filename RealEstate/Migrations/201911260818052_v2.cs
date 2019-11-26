namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ad", "Property_Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ad", "Property_Type", c => c.String(maxLength: 50));
        }
    }
}

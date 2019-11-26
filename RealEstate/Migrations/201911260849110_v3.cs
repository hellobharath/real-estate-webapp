namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ad", "PropertyDesc", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ad", "PropertyDesc");
        }
    }
}

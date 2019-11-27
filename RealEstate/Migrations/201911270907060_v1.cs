namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Property_Id = c.String(maxLength: 128),
                        Status = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                        PropertyDesc = c.String(maxLength: 200),
                        Date_Posted = c.DateTime(storeType: "smalldatetime"),
                        Ad_Type = c.String(maxLength: 50),
                        Price = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Property", t => t.Property_Id)
                .Index(t => t.Property_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Agreement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 50),
                        Agreement_Terms = c.String(),
                        Token_Advance = c.Int(),
                        Total_Amt = c.Int(),
                        Agreement_Duration = c.Int(),
                        Payer_Id = c.String(maxLength: 128),
                        Payee_Id = c.String(maxLength: 128),
                        Status = c.String(maxLength: 50),
                        Property_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Property", t => t.Property_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Payee_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Payer_Id)
                .Index(t => t.Payer_Id)
                .Index(t => t.Payee_Id)
                .Index(t => t.Property_Id);
            
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Category = c.String(maxLength: 50),
                        Location = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        Owner_Id = c.String(maxLength: 128),
                        Status = c.String(maxLength: 50),
                        image1 = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Plot",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Length = c.Int(),
                        Width = c.Int(),
                        Price_Per_Sqft = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Property", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Residential",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Type = c.String(maxLength: 50),
                        Built_Up_Length = c.Int(),
                        Built_Up_Width = c.Int(),
                        Land_Length = c.Int(),
                        Land_Width = c.Int(),
                        NoOfBedrooms = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Property", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Agreement", "Payer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Agreement", "Payee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Residential", "Id", "dbo.Property");
            DropForeignKey("dbo.Plot", "Id", "dbo.Property");
            DropForeignKey("dbo.Property", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Agreement", "Property_Id", "dbo.Property");
            DropForeignKey("dbo.Ad", "Property_Id", "dbo.Property");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ad", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Residential", new[] { "Id" });
            DropIndex("dbo.Plot", new[] { "Id" });
            DropIndex("dbo.Property", new[] { "Owner_Id" });
            DropIndex("dbo.Agreement", new[] { "Property_Id" });
            DropIndex("dbo.Agreement", new[] { "Payee_Id" });
            DropIndex("dbo.Agreement", new[] { "Payer_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Ad", new[] { "Owner_Id" });
            DropIndex("dbo.Ad", new[] { "Property_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Residential");
            DropTable("dbo.Plot");
            DropTable("dbo.Property");
            DropTable("dbo.Agreement");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Ad");
        }
    }
}

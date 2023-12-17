namespace KEYSAFE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TenantAccountCategories",
                c => new
                    {
                        SysId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Name = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SysId);
            
            CreateTable(
                "dbo.TenantAccountPasswords",
                c => new
                    {
                        SysId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        TenantAccountId = c.Guid(nullable: false),
                        Password = c.String(),
                        Length = c.Int(nullable: false),
                        Generated = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SysId)
                .ForeignKey("dbo.TenantAccounts", t => t.TenantAccountId, cascadeDelete: true)
                .Index(t => t.TenantAccountId);
            
            CreateTable(
                "dbo.TenantAccounts",
                c => new
                    {
                        SysId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Name = c.String(nullable: false),
                        TenantAccountCategoryId = c.Guid(),
                        TenantAccountWebsiteId = c.Guid(),
                        Username = c.String(),
                        EmailAddress = c.String(),
                        MobileNumber = c.String(),
                        SecurityType = c.Int(nullable: false),
                        Comments = c.String(),
                        Closed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SysId)
                .ForeignKey("dbo.TenantAccountCategories", t => t.TenantAccountCategoryId)
                .ForeignKey("dbo.TenantAccountWebsites", t => t.TenantAccountWebsiteId)
                .Index(t => t.TenantAccountCategoryId)
                .Index(t => t.TenantAccountWebsiteId);
            
            CreateTable(
                "dbo.TenantAccountWebsites",
                c => new
                    {
                        SysId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Name = c.String(nullable: false),
                        Url = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SysId);
            
            CreateTable(
                "dbo.TenantAccountPins",
                c => new
                    {
                        SysId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        TenantAccountId = c.Guid(nullable: false),
                        Pin = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                        Generated = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SysId)
                .ForeignKey("dbo.TenantAccounts", t => t.TenantAccountId, cascadeDelete: true)
                .Index(t => t.TenantAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TenantAccountPins", "TenantAccountId", "dbo.TenantAccounts");
            DropForeignKey("dbo.TenantAccountPasswords", "TenantAccountId", "dbo.TenantAccounts");
            DropForeignKey("dbo.TenantAccounts", "TenantAccountWebsiteId", "dbo.TenantAccountWebsites");
            DropForeignKey("dbo.TenantAccounts", "TenantAccountCategoryId", "dbo.TenantAccountCategories");
            DropIndex("dbo.TenantAccountPins", new[] { "TenantAccountId" });
            DropIndex("dbo.TenantAccounts", new[] { "TenantAccountWebsiteId" });
            DropIndex("dbo.TenantAccounts", new[] { "TenantAccountCategoryId" });
            DropIndex("dbo.TenantAccountPasswords", new[] { "TenantAccountId" });
            DropTable("dbo.TenantAccountPins");
            DropTable("dbo.TenantAccountWebsites");
            DropTable("dbo.TenantAccounts");
            DropTable("dbo.TenantAccountPasswords");
            DropTable("dbo.TenantAccountCategories");
        }
    }
}

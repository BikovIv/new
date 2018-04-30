namespace LostStuffsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        LostStuffId = c.Int(nullable: false),
                        UserId = c.String(),
                        UserName = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LostStuffs", t => t.LostStuffId, cascadeDelete: true)
                .Index(t => t.LostStuffId);
            
            CreateTable(
                "dbo.LostStuffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        ImageName = c.String(),
                        ImagePath = c.String(),
                        PhoneNumber = c.String(),
                        UserId = c.String(),
                        UserName = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "LostStuffId", "dbo.LostStuffs");
            DropIndex("dbo.Comments", new[] { "LostStuffId" });
            DropTable("dbo.LostStuffs");
            DropTable("dbo.Comments");
        }
    }
}

namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        authorID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        address = c.String(nullable: false, maxLength: 100),
                        phone = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.authorID);
            
            CreateTable(
                "dbo.BookWritings",
                c => new
                    {
                        index = c.Int(nullable: false, identity: true),
                        authorID = c.Int(nullable: false),
                        bookID = c.Int(nullable: false),
                        role = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.index)
                .ForeignKey("dbo.Authors", t => t.authorID, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.bookID, cascadeDelete: true)
                .Index(t => t.authorID)
                .Index(t => t.bookID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        bookID = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 50),
                        unit = c.String(nullable: false, maxLength: 24),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        description = c.String(nullable: false, maxLength: 50),
                        image = c.String(nullable: false),
                        updateDate = c.DateTime(nullable: false),
                        sellNumber = c.Int(nullable: false),
                        subjectID = c.Int(nullable: false),
                        publisherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.bookID)
                .ForeignKey("dbo.Publishers", t => t.publisherID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.subjectID, cascadeDelete: true)
                .Index(t => t.subjectID)
                .Index(t => t.publisherID);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        publisherID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        address = c.String(nullable: false, maxLength: 150),
                        phone = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.publisherID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        subjectID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.subjectID);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        billID = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        delivered = c.Boolean(nullable: false),
                        deliveryDate = c.DateTime(nullable: false),
                        nameCustomer = c.String(maxLength: 50),
                        deliveryAddress = c.String(maxLength: 50),
                        shipPhone = c.String(maxLength: 15),
                        payment = c.Boolean(nullable: false),
                        deliveryForm = c.Boolean(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.billID)
                .ForeignKey("dbo.User", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(maxLength: 24),
                        Address = c.String(maxLength: 40),
                        Phone = c.String(maxLength: 15),
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
                "dbo.Information_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Login_Management",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User_Authorization",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.DetailsOrders",
                c => new
                    {
                        index = c.Int(nullable: false, identity: true),
                        billID = c.Int(nullable: false),
                        bookID = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        totalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.index)
                .ForeignKey("dbo.Bills", t => t.billID, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.bookID, cascadeDelete: true)
                .Index(t => t.billID)
                .Index(t => t.bookID);
            
            CreateTable(
                "dbo.Role",
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
            DropForeignKey("dbo.User_Authorization", "RoleId", "dbo.Role");
            DropForeignKey("dbo.DetailsOrders", "bookID", "dbo.Books");
            DropForeignKey("dbo.DetailsOrders", "billID", "dbo.Bills");
            DropForeignKey("dbo.User_Authorization", "UserId", "dbo.User");
            DropForeignKey("dbo.Login_Management", "UserId", "dbo.User");
            DropForeignKey("dbo.Information_User", "UserId", "dbo.User");
            DropForeignKey("dbo.Bills", "Id", "dbo.User");
            DropForeignKey("dbo.BookWritings", "bookID", "dbo.Books");
            DropForeignKey("dbo.Books", "subjectID", "dbo.Subjects");
            DropForeignKey("dbo.Books", "publisherID", "dbo.Publishers");
            DropForeignKey("dbo.BookWritings", "authorID", "dbo.Authors");
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.DetailsOrders", new[] { "bookID" });
            DropIndex("dbo.DetailsOrders", new[] { "billID" });
            DropIndex("dbo.User_Authorization", new[] { "RoleId" });
            DropIndex("dbo.User_Authorization", new[] { "UserId" });
            DropIndex("dbo.Login_Management", new[] { "UserId" });
            DropIndex("dbo.Information_User", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.Bills", new[] { "Id" });
            DropIndex("dbo.Books", new[] { "publisherID" });
            DropIndex("dbo.Books", new[] { "subjectID" });
            DropIndex("dbo.BookWritings", new[] { "bookID" });
            DropIndex("dbo.BookWritings", new[] { "authorID" });
            DropTable("dbo.Role");
            DropTable("dbo.DetailsOrders");
            DropTable("dbo.User_Authorization");
            DropTable("dbo.Login_Management");
            DropTable("dbo.Information_User");
            DropTable("dbo.User");
            DropTable("dbo.Bills");
            DropTable("dbo.Subjects");
            DropTable("dbo.Publishers");
            DropTable("dbo.Books");
            DropTable("dbo.BookWritings");
            DropTable("dbo.Authors");
        }
    }
}

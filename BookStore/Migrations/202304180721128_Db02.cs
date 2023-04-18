namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookWritings", "authorID", "dbo.Authors");
            DropForeignKey("dbo.BookWritings", "bookID", "dbo.Books");
            DropIndex("dbo.BookWritings", new[] { "authorID" });
            DropIndex("dbo.BookWritings", new[] { "bookID" });
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        VoucherID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.VoucherID);
            
            AddColumn("dbo.Books", "Author_authorID", c => c.Int());
            AddColumn("dbo.Bills", "VoucherID", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "Author_authorID");
            CreateIndex("dbo.Bills", "VoucherID");
            AddForeignKey("dbo.Books", "Author_authorID", "dbo.Authors", "authorID");
            AddForeignKey("dbo.Bills", "VoucherID", "dbo.Vouchers", "VoucherID", cascadeDelete: true);
            DropTable("dbo.BookWritings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookWritings",
                c => new
                    {
                        index = c.Int(nullable: false, identity: true),
                        authorID = c.Int(nullable: false),
                        bookID = c.Int(nullable: false),
                        role = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.index);
            
            DropForeignKey("dbo.Bills", "VoucherID", "dbo.Vouchers");
            DropForeignKey("dbo.Books", "Author_authorID", "dbo.Authors");
            DropIndex("dbo.Bills", new[] { "VoucherID" });
            DropIndex("dbo.Books", new[] { "Author_authorID" });
            DropColumn("dbo.Bills", "VoucherID");
            DropColumn("dbo.Books", "Author_authorID");
            DropTable("dbo.Vouchers");
            CreateIndex("dbo.BookWritings", "bookID");
            CreateIndex("dbo.BookWritings", "authorID");
            AddForeignKey("dbo.BookWritings", "bookID", "dbo.Books", "bookID", cascadeDelete: true);
            AddForeignKey("dbo.BookWritings", "authorID", "dbo.Authors", "authorID", cascadeDelete: true);
        }
    }
}

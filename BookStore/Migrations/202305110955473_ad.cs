namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ad : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "FullName", c => c.String(nullable: false, maxLength: 24));
            AlterColumn("dbo.User", "Address", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.User", "Phone", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Phone", c => c.String(maxLength: 15));
            AlterColumn("dbo.User", "Address", c => c.String(maxLength: 40));
            AlterColumn("dbo.User", "FullName", c => c.String(maxLength: 24));
        }
    }
}

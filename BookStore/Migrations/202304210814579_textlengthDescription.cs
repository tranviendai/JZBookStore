namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class textlengthDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "description", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "description", c => c.String(nullable: false, maxLength: 50));
        }
    }
}

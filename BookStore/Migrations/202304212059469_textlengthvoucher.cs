namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class textlengthvoucher : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vouchers", "Name", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vouchers", "Name", c => c.String(maxLength: 20));
        }
    }
}

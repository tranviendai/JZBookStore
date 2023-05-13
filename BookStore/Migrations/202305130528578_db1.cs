﻿namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "image", c => c.String(nullable: false));
        }
    }
}

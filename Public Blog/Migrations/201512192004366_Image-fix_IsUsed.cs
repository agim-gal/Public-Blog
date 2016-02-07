namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Imagefix_IsUsed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "IsUsed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "IsUsed");
        }
    }
}

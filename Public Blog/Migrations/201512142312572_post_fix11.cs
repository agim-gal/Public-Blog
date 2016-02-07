namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class post_fix11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Header", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Header");
        }
    }
}

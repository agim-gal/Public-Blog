namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posts_add_image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "image", c => c.Binary(storeType: "image"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "image");
        }
    }
}

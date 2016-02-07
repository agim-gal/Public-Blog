namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posts_add_image_fix3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ImageMimeType");
        }
    }
}

namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posts_add_image_fix2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ImageData", c => c.Binary(storeType: "image"));
            DropColumn("dbo.Posts", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Image", c => c.Binary(storeType: "image"));
            DropColumn("dbo.Posts", "ImageData");
        }
    }
}

namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_post : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "Image_Id_image" });
            DropIndex("dbo.Posts", new[] { "User_Id_user" });
            CreateIndex("dbo.Posts", "image_Id_image");
            CreateIndex("dbo.Posts", "user_Id_user");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "user_Id_user" });
            DropIndex("dbo.Posts", new[] { "image_Id_image" });
            CreateIndex("dbo.Posts", "User_Id_user");
            CreateIndex("dbo.Posts", "Image_Id_image");
        }
    }
}

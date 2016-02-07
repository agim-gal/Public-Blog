namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Images_and_image_in_post : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id_image = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(storeType: "image"),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.Id_image);
            
            AddColumn("dbo.Posts", "Image_Id_image", c => c.Int());
            CreateIndex("dbo.Posts", "Image_Id_image");
            AddForeignKey("dbo.Posts", "Image_Id_image", "dbo.Images", "Id_image");
            DropColumn("dbo.Posts", "ImageData");
            DropColumn("dbo.Posts", "ImageMimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ImageMimeType", c => c.String());
            AddColumn("dbo.Posts", "ImageData", c => c.Binary(storeType: "image"));
            DropForeignKey("dbo.Posts", "Image_Id_image", "dbo.Images");
            DropIndex("dbo.Posts", new[] { "Image_Id_image" });
            DropColumn("dbo.Posts", "Image_Id_image");
            DropTable("dbo.Images");
        }
    }
}

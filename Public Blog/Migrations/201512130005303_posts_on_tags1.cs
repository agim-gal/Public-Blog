namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posts_on_tags1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_name = c.String(nullable: false, maxLength: 128),
                        Post_Id_post = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_name, t.Post_Id_post })
                .ForeignKey("dbo.Tags", t => t.Tag_name, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_Id_post, cascadeDelete: true)
                .Index(t => t.Tag_name)
                .Index(t => t.Post_Id_post);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPosts", "Post_Id_post", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_name", "dbo.Tags");
            DropIndex("dbo.TagPosts", new[] { "Post_Id_post" });
            DropIndex("dbo.TagPosts", new[] { "Tag_name" });
            DropTable("dbo.TagPosts");
        }
    }
}

namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class post_fix_no_nullable_field : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "User_Id_user", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "User_Id_user" });
            AlterColumn("dbo.Posts", "Header", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Posts", "User_Id_user", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "User_Id_user");
            AddForeignKey("dbo.Posts", "User_Id_user", "dbo.Users", "Id_user", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "User_Id_user", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "User_Id_user" });
            AlterColumn("dbo.Posts", "User_Id_user", c => c.Int());
            AlterColumn("dbo.Posts", "Header", c => c.String(maxLength: 100));
            CreateIndex("dbo.Posts", "User_Id_user");
            AddForeignKey("dbo.Posts", "User_Id_user", "dbo.Users", "Id_user");
        }
    }
}

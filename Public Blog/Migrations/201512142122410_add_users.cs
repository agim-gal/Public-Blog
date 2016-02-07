namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_users : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id_user = c.Int(nullable: false, identity: true),
                        Login = c.String(maxLength: 30),
                        Password = c.String(),
                        Email = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id_user);
            
            AddColumn("dbo.Posts", "User_Id_user", c => c.Int());
            CreateIndex("dbo.Posts", "User_Id_user");
            AddForeignKey("dbo.Posts", "User_Id_user", "dbo.Users", "Id_user");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "User_Id_user", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "User_Id_user" });
            DropColumn("dbo.Posts", "User_Id_user");
            DropTable("dbo.Users");
        }
    }
}

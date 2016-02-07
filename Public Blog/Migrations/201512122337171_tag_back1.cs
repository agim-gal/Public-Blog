namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tag_back1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tags", new[] { "name" });
            AlterColumn("dbo.Tags", "name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "name", c => c.Int(nullable: false));
            CreateIndex("dbo.Tags", "name", unique: true);
        }
    }
}

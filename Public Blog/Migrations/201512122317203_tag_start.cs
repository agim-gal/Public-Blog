namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tag_start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id_tag = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id_tag);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tags", new[] { "name" });
            DropTable("dbo.Tags");
        }
    }
}

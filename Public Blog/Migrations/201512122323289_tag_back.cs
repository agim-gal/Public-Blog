namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tag_back : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Tags");
            AddColumn("dbo.Tags", "Id_tag", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Tags", "name", c => c.String());
            AddPrimaryKey("dbo.Tags", "Id_tag");
            DropColumn("dbo.Tags", "_tag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "_tag", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Tags");
            AlterColumn("dbo.Tags", "name", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Tags", "Id_tag");
            AddPrimaryKey("dbo.Tags", "name");
        }
    }
}

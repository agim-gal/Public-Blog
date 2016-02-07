namespace Public_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class name_is_key : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Tags");
            AlterColumn("dbo.Tags", "name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Tags", "name");
            DropColumn("dbo.Tags", "Id_tag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "Id_tag", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Tags");
            AlterColumn("dbo.Tags", "name", c => c.String());
            AddPrimaryKey("dbo.Tags", "Id_tag");
        }
    }
}

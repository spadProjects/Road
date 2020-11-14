namespace Road.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Editedstaticcontent : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StaticContentDetails", "Identifier");
            DropColumn("dbo.StaticContentTypes", "Identifier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StaticContentTypes", "Identifier", c => c.String(nullable: false, maxLength: 600));
            AddColumn("dbo.StaticContentDetails", "Identifier", c => c.String(nullable: false, maxLength: 600));
        }
    }
}

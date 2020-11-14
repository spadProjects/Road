namespace Road.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Editedprojects : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Projects", "ViewCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "ViewCount", c => c.Int(nullable: false));
        }
    }
}

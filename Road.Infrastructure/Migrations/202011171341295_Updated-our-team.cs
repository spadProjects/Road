namespace Road.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedourteam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OurTeams", "Instagram", c => c.String());
            DropColumn("dbo.OurTeams", "Linkedin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OurTeams", "Linkedin", c => c.String());
            DropColumn("dbo.OurTeams", "Instagram");
        }
    }
}

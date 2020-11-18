namespace Road.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProjects : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Customer", c => c.String(maxLength: 600));
            AddColumn("dbo.Projects", "Budget", c => c.String(maxLength: 600));
            AddColumn("dbo.Projects", "SurfaceArea", c => c.String(maxLength: 600));
            AddColumn("dbo.Projects", "CompletedDate", c => c.String(maxLength: 600));
            AddColumn("dbo.Projects", "FirstConsultant", c => c.String(maxLength: 600));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "FirstConsultant");
            DropColumn("dbo.Projects", "CompletedDate");
            DropColumn("dbo.Projects", "SurfaceArea");
            DropColumn("dbo.Projects", "Budget");
            DropColumn("dbo.Projects", "Customer");
        }
    }
}

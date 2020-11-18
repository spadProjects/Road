namespace Road.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Editedprojectgallery : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectGalleries", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectGalleries", new[] { "ProjectId" });
            AlterColumn("dbo.ProjectGalleries", "ProjectId", c => c.Int());
            AlterColumn("dbo.ProjectGalleries", "Title", c => c.String(nullable: false));
            CreateIndex("dbo.ProjectGalleries", "ProjectId");
            AddForeignKey("dbo.ProjectGalleries", "ProjectId", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectGalleries", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectGalleries", new[] { "ProjectId" });
            AlterColumn("dbo.ProjectGalleries", "Title", c => c.String());
            AlterColumn("dbo.ProjectGalleries", "ProjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProjectGalleries", "ProjectId");
            AddForeignKey("dbo.ProjectGalleries", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}

namespace Road.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTestimonial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Testimonials", "Company", c => c.String(nullable: false));
            AddColumn("dbo.Testimonials", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Testimonials", "Image");
            DropColumn("dbo.Testimonials", "Company");
        }
    }
}

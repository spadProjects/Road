namespace Road.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedContactForm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactForms", "ServiceId", "dbo.Services");
            DropIndex("dbo.ContactForms", new[] { "ServiceId" });
            AlterColumn("dbo.ContactForms", "Message", c => c.String(nullable: false));
            DropColumn("dbo.ContactForms", "ServiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactForms", "ServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.ContactForms", "Message", c => c.String());
            CreateIndex("dbo.ContactForms", "ServiceId");
            AddForeignKey("dbo.ContactForms", "ServiceId", "dbo.Services", "Id", cascadeDelete: true);
        }
    }
}

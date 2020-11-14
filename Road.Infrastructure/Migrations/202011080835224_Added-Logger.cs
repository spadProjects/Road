namespace Road.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLogger : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 200),
                        TableName = c.String(maxLength: 100),
                        EntityId = c.Int(nullable: false),
                        Action = c.String(maxLength: 100),
                        ActionDate = c.DateTime(nullable: false),
                        OldValue = c.String(),
                        NewValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ArticleCategories", "InsertUser", c => c.String());
            AddColumn("dbo.ArticleCategories", "InsertDate", c => c.DateTime());
            AddColumn("dbo.ArticleCategories", "UpdateUser", c => c.String());
            AddColumn("dbo.ArticleCategories", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.ArticleCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Articles", "InsertUser", c => c.String());
            AddColumn("dbo.Articles", "InsertDate", c => c.DateTime());
            AddColumn("dbo.Articles", "UpdateUser", c => c.String());
            AddColumn("dbo.Articles", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Articles", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ArticleComments", "InsertUser", c => c.String());
            AddColumn("dbo.ArticleComments", "InsertDate", c => c.DateTime());
            AddColumn("dbo.ArticleComments", "UpdateUser", c => c.String());
            AddColumn("dbo.ArticleComments", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.ArticleComments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ArticleHeadLines", "InsertUser", c => c.String());
            AddColumn("dbo.ArticleHeadLines", "InsertDate", c => c.DateTime());
            AddColumn("dbo.ArticleHeadLines", "UpdateUser", c => c.String());
            AddColumn("dbo.ArticleHeadLines", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.ArticleHeadLines", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ArticleTags", "InsertUser", c => c.String());
            AddColumn("dbo.ArticleTags", "InsertDate", c => c.DateTime());
            AddColumn("dbo.ArticleTags", "UpdateUser", c => c.String());
            AddColumn("dbo.ArticleTags", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.ArticleTags", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ContactForms", "InsertUser", c => c.String());
            AddColumn("dbo.ContactForms", "InsertDate", c => c.DateTime());
            AddColumn("dbo.ContactForms", "UpdateUser", c => c.String());
            AddColumn("dbo.ContactForms", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.ContactForms", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Services", "InsertUser", c => c.String());
            AddColumn("dbo.Services", "InsertDate", c => c.DateTime());
            AddColumn("dbo.Services", "UpdateUser", c => c.String());
            AddColumn("dbo.Services", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Services", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ServiceIncludes", "InsertUser", c => c.String());
            AddColumn("dbo.ServiceIncludes", "InsertDate", c => c.DateTime());
            AddColumn("dbo.ServiceIncludes", "UpdateUser", c => c.String());
            AddColumn("dbo.ServiceIncludes", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.ServiceIncludes", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Faqs", "InsertUser", c => c.String());
            AddColumn("dbo.Faqs", "InsertDate", c => c.DateTime());
            AddColumn("dbo.Faqs", "UpdateUser", c => c.String());
            AddColumn("dbo.Faqs", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Faqs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Galleries", "InsertUser", c => c.String());
            AddColumn("dbo.Galleries", "InsertDate", c => c.DateTime());
            AddColumn("dbo.Galleries", "UpdateUser", c => c.String());
            AddColumn("dbo.Galleries", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Galleries", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.OurTeams", "InsertUser", c => c.String());
            AddColumn("dbo.OurTeams", "InsertDate", c => c.DateTime());
            AddColumn("dbo.OurTeams", "UpdateUser", c => c.String());
            AddColumn("dbo.OurTeams", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.OurTeams", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Partners", "InsertUser", c => c.String());
            AddColumn("dbo.Partners", "InsertDate", c => c.DateTime());
            AddColumn("dbo.Partners", "UpdateUser", c => c.String());
            AddColumn("dbo.Partners", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Partners", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.StaticContentDetails", "InsertUser", c => c.String());
            AddColumn("dbo.StaticContentDetails", "InsertDate", c => c.DateTime());
            AddColumn("dbo.StaticContentDetails", "UpdateUser", c => c.String());
            AddColumn("dbo.StaticContentDetails", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.StaticContentDetails", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.StaticContentTypes", "InsertUser", c => c.String());
            AddColumn("dbo.StaticContentTypes", "InsertDate", c => c.DateTime());
            AddColumn("dbo.StaticContentTypes", "UpdateUser", c => c.String());
            AddColumn("dbo.StaticContentTypes", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.StaticContentTypes", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Testimonials", "InsertUser", c => c.String());
            AddColumn("dbo.Testimonials", "InsertDate", c => c.DateTime());
            AddColumn("dbo.Testimonials", "UpdateUser", c => c.String());
            AddColumn("dbo.Testimonials", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Testimonials", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Testimonials", "IsDeleted");
            DropColumn("dbo.Testimonials", "UpdateDate");
            DropColumn("dbo.Testimonials", "UpdateUser");
            DropColumn("dbo.Testimonials", "InsertDate");
            DropColumn("dbo.Testimonials", "InsertUser");
            DropColumn("dbo.StaticContentTypes", "IsDeleted");
            DropColumn("dbo.StaticContentTypes", "UpdateDate");
            DropColumn("dbo.StaticContentTypes", "UpdateUser");
            DropColumn("dbo.StaticContentTypes", "InsertDate");
            DropColumn("dbo.StaticContentTypes", "InsertUser");
            DropColumn("dbo.StaticContentDetails", "IsDeleted");
            DropColumn("dbo.StaticContentDetails", "UpdateDate");
            DropColumn("dbo.StaticContentDetails", "UpdateUser");
            DropColumn("dbo.StaticContentDetails", "InsertDate");
            DropColumn("dbo.StaticContentDetails", "InsertUser");
            DropColumn("dbo.Partners", "IsDeleted");
            DropColumn("dbo.Partners", "UpdateDate");
            DropColumn("dbo.Partners", "UpdateUser");
            DropColumn("dbo.Partners", "InsertDate");
            DropColumn("dbo.Partners", "InsertUser");
            DropColumn("dbo.OurTeams", "IsDeleted");
            DropColumn("dbo.OurTeams", "UpdateDate");
            DropColumn("dbo.OurTeams", "UpdateUser");
            DropColumn("dbo.OurTeams", "InsertDate");
            DropColumn("dbo.OurTeams", "InsertUser");
            DropColumn("dbo.Galleries", "IsDeleted");
            DropColumn("dbo.Galleries", "UpdateDate");
            DropColumn("dbo.Galleries", "UpdateUser");
            DropColumn("dbo.Galleries", "InsertDate");
            DropColumn("dbo.Galleries", "InsertUser");
            DropColumn("dbo.Faqs", "IsDeleted");
            DropColumn("dbo.Faqs", "UpdateDate");
            DropColumn("dbo.Faqs", "UpdateUser");
            DropColumn("dbo.Faqs", "InsertDate");
            DropColumn("dbo.Faqs", "InsertUser");
            DropColumn("dbo.ServiceIncludes", "IsDeleted");
            DropColumn("dbo.ServiceIncludes", "UpdateDate");
            DropColumn("dbo.ServiceIncludes", "UpdateUser");
            DropColumn("dbo.ServiceIncludes", "InsertDate");
            DropColumn("dbo.ServiceIncludes", "InsertUser");
            DropColumn("dbo.Services", "IsDeleted");
            DropColumn("dbo.Services", "UpdateDate");
            DropColumn("dbo.Services", "UpdateUser");
            DropColumn("dbo.Services", "InsertDate");
            DropColumn("dbo.Services", "InsertUser");
            DropColumn("dbo.ContactForms", "IsDeleted");
            DropColumn("dbo.ContactForms", "UpdateDate");
            DropColumn("dbo.ContactForms", "UpdateUser");
            DropColumn("dbo.ContactForms", "InsertDate");
            DropColumn("dbo.ContactForms", "InsertUser");
            DropColumn("dbo.ArticleTags", "IsDeleted");
            DropColumn("dbo.ArticleTags", "UpdateDate");
            DropColumn("dbo.ArticleTags", "UpdateUser");
            DropColumn("dbo.ArticleTags", "InsertDate");
            DropColumn("dbo.ArticleTags", "InsertUser");
            DropColumn("dbo.ArticleHeadLines", "IsDeleted");
            DropColumn("dbo.ArticleHeadLines", "UpdateDate");
            DropColumn("dbo.ArticleHeadLines", "UpdateUser");
            DropColumn("dbo.ArticleHeadLines", "InsertDate");
            DropColumn("dbo.ArticleHeadLines", "InsertUser");
            DropColumn("dbo.ArticleComments", "IsDeleted");
            DropColumn("dbo.ArticleComments", "UpdateDate");
            DropColumn("dbo.ArticleComments", "UpdateUser");
            DropColumn("dbo.ArticleComments", "InsertDate");
            DropColumn("dbo.ArticleComments", "InsertUser");
            DropColumn("dbo.Articles", "IsDeleted");
            DropColumn("dbo.Articles", "UpdateDate");
            DropColumn("dbo.Articles", "UpdateUser");
            DropColumn("dbo.Articles", "InsertDate");
            DropColumn("dbo.Articles", "InsertUser");
            DropColumn("dbo.ArticleCategories", "IsDeleted");
            DropColumn("dbo.ArticleCategories", "UpdateDate");
            DropColumn("dbo.ArticleCategories", "UpdateUser");
            DropColumn("dbo.ArticleCategories", "InsertDate");
            DropColumn("dbo.ArticleCategories", "InsertUser");
            DropTable("dbo.Logs");
        }
    }
}

﻿using Road.Core.Models;
using Road.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Road.Infrastructure.Repositories
{
    public class ArticlesRepository : BaseRepository<Article, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ArticlesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public Article GetArticle(int id)
        {
            return _context.Articles.Include(a=>a.User).Include(a=>a.ArticleCategory).Include(a=>a.ArticleHeadLines).FirstOrDefault(a=>a.Id == id);
        }

        public ArticleCategory GetArticleCategory(int categoryId)
        {
            return _context.ArticleCategories.Find(categoryId);
        }
        public List<Article> GetArticlesByCategoryId(int categoryId,int? take = null)
        {
            var articles = new List<Article>();

            if (take == null)
                articles = _context.Articles.Where(a => a.IsDeleted == false && a.ArticleCategoryId == categoryId).Include(a => a.User).Include(a => a.ArticleCategory).OrderByDescending(a => a.AddedDate).ToList();
            else
                articles = _context.Articles.Where(a => a.IsDeleted == false && a.ArticleCategoryId == categoryId).Take(take.Value).Include(a => a.User).Include(a => a.ArticleCategory).OrderByDescending(a => a.AddedDate).ToList();

            return articles;
        }

        public List<ArticleComment> GetArticleComments(int articleId)
        {
            return _context.ArticleComments.Where(c => c.IsDeleted == false && c.ArticleId == articleId).ToList();
        }
        public List<ArticleTag> GetArticleTags(int articleId)
        {
            return _context.ArticleTags.Where(c => c.IsDeleted == false && c.ArticleId == articleId).ToList();
        }
        public List<ArticleHeadLine> GetArticleHeadlines(int articleId)
        {
            return _context.ArticleHeadLines.Where(c => c.IsDeleted == false && c.ArticleId == articleId).ToList();
        }
        public List<Article> GetArticles(int? take = null)
        {
            var articles = new List<Article>();

            if (take == null)
                articles = _context.Articles.Where(a => a.IsDeleted == false).Include(a => a.User)
                    .Include(a => a.ArticleCategory).OrderByDescending(a => a.AddedDate).ToList();
            else
                articles = _context.Articles.Where(a => a.IsDeleted == false).Take(take.Value).Include(a => a.User)
                    .Include(a => a.ArticleCategory).OrderByDescending(a => a.AddedDate).ToList();

            return articles;
        }
        public List<ArticleCategory> GetArticleCategories()
        {
            return _context.ArticleCategories.Where(a => a.IsDeleted == false).ToList();
        }
        public void AddArticle(Article article)
        {
            var user = GetCurrentUser();
            article.InsertDate = DateTime.Now;
            article.InsertUser = user.UserName;
            article.AddedDate = DateTime.Now;
            article.UserId = user.Id;
            _context.Articles.Add(article);
            _context.SaveChanges();
            _logger.LogEvent(article.GetType().Name, article.Id, "Add");
        }
        public string GetArticleTagsStr(int articleId)
        {
            var articleTags = _context.ArticleTags.Where(t => t.ArticleId == articleId && t.IsDeleted == false).Select(t=>t.Title).ToList();
            var tagsStr = string.Join("-", articleTags.ToList());
            return tagsStr;
        }
        public void AddArticleTags(int articleId, string articleTags)
        {
            if (string.IsNullOrEmpty(articleTags))
                return;
            var oldTags = _context.ArticleTags.Where(t => t.ArticleId == articleId).ToList();
            foreach (var tag in oldTags)
            {
                _context.ArticleTags.Remove(tag);
                _context.SaveChanges();
            }

            string[] tagsArr = articleTags.Trim().Split('-');
            foreach (var tag in tagsArr)
            {
                var tagObj = new ArticleTag();
                tagObj.ArticleId = articleId;
                tagObj.Title = tag.Trim();

                _context.ArticleTags.Add(tagObj);
                _context.SaveChanges();
            }
        }
        public void AddArticleHeadLine(ArticleHeadLine headLine)
        {
            var user = GetCurrentUser();
            headLine.InsertDate = DateTime.Now;
            headLine.InsertUser = user.UserName;
            _context.ArticleHeadLines.Add(headLine);
            _context.SaveChanges();
            _logger.LogEvent(headLine.GetType().Name, headLine.Id, "Add");
        }
        public List<Article> GetByCategory(int categoryId)
        {
            return _context.Articles.Where(a => a.IsDeleted == false && a.ArticleCategoryId == categoryId).Include(a => a.User).Include(a => a.ArticleCategory).OrderByDescending(a => a.AddedDate).ToList();
        }

        public void AddComment(ArticleComment comment)
        {
            _context.ArticleComments.Add(comment);
            _context.SaveChanges();
        }
        public void UpdateArticleViewCount(int articleId)
        {
            var article = _context.Articles.Find(articleId);
            article.ViewCount++;
            _context.Entry(article).State = EntityState.Modified;
            _context.SaveChanges();
        }
        //public Article DeleteArticle(int articleId)
        //{
        //    var article = _context.Articles.Find(articleId);
        //    if (article == null)
        //        return null;
        //    var articleTags = _context.ArticleTags.Where(t => t.ArticleId == articleId).ToList();
        //    var articeheadLines = _context.
        //}
    }
}

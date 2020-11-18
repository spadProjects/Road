using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Road.Core.Models;
using Road.Infrastructure.Repositories;
using Road.Web.ViewModels;

namespace Road.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly ArticlesRepository _repo;

        public BlogController(ArticlesRepository repo)
        {
            _repo = repo;
        }
        [Route("Blog/{id}/{title}")]
        public ActionResult Index(int id, int pageNumber = 1, string searchString = null)
        {
            var take = 3;
            var articleListVm = new List<ArticleListViewModel>();
            var category = _repo.GetArticleCategory(id);
            var articles = _repo.GetArticlesByCategoryId(id);

            if (!string.IsNullOrEmpty(searchString))
                articles = articles.Where(a => a.Title.ToLower().Trim().Contains(searchString.ToLower().Trim()) || a.ShortDescription.ToLower().Trim().Contains(searchString.ToLower().Trim()))
                    .ToList();

            int pageCount = articles.Count / take + 1;
            ViewBag.PageCount = pageCount;
            ViewBag.CategoryId = category.Id;
            ViewBag.CategoryName = category.Title;
            ViewBag.CurrentPage = pageNumber;

            var skip = (pageNumber - 1) * take;
            articles = articles.Skip(skip).Take(take).ToList();
            foreach (var item in articles)
            {
                var vm = new ArticleListViewModel(item);
                var commentCount = _repo.GetArticleComments(item.Id).Count;
                vm.CommentCount = commentCount;
                articleListVm.Add(vm);
            }
            #region BreadCrumb
            var breadCrumbVm = new List<BreadCrumbViewModel>();
            breadCrumbVm.Add(new BreadCrumbViewModel() { Title = "بلاگ", Href = "/Blog" });
            breadCrumbVm.Add(new BreadCrumbViewModel() { Title = category.Title, Href = $"/Blog/{category.Id}/{category.Title.Replace(" ", "-")}" });
            if (!string.IsNullOrEmpty(searchString))
            {
                breadCrumbVm.Add(new BreadCrumbViewModel() { Title = $"جستجو : {searchString}", Href = "#" });
            }
            ViewBag.BreadCrumb = breadCrumbVm;
            #endregion
            return View(articleListVm);
        }
        [Route("Blog")]
        public ActionResult SearchArticle(string searchString = null, int pageNumber = 1)
        {
            var take = 3;
            var articles = new List<Article>();
            var articleListVm = new List<ArticleListViewModel>();

            if (!string.IsNullOrEmpty(searchString))
                articles = _repo.GetArticles().Where(a =>
                        a.Title.ToLower().Trim().Contains(searchString.ToLower().Trim()) || a.ShortDescription.ToLower()
                            .Trim().Contains(searchString.ToLower().Trim()))
                    .ToList();
            else
                articles = _repo.GetArticles();

            int pageCount = articles.Count / take + 1;
            ViewBag.SearchString = searchString;
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = pageNumber;

            var skip = (pageNumber - 1) * take;
            articles = articles.Skip(skip).Take(take).ToList();
            foreach (var item in articles)
            {
                var vm = new ArticleListViewModel(item);
                var commentCount = _repo.GetArticleComments(item.Id).Count;
                vm.CommentCount = commentCount;
                articleListVm.Add(vm);
            }
            #region BreadCrumb
            var breadCrumbVm = new List<BreadCrumbViewModel>();
            breadCrumbVm.Add(new BreadCrumbViewModel() { Title = "بلاگ", Href = "/Blog" });
            if (!string.IsNullOrEmpty(searchString))
                breadCrumbVm.Add(new BreadCrumbViewModel() { Title = $"جستجو : {searchString}", Href = "#" });

            ViewBag.BreadCrumb = breadCrumbVm;
            #endregion

            return View(articleListVm);
        }
        [Route("Blog/Post/{id}/{title}")]
        [Route("Blog/Details/{id}")]
        public ActionResult Details(int id)
        {
            var article = _repo.GetArticle(id);
            var articleDetailsVm = new ArticleDetailsViewModel(article);
            var articleComments = _repo.GetArticleComments(id);
            var articleCommentsVm = new List<ArticleCommentViewModel>();

            foreach (var item in articleComments)
                articleCommentsVm.Add(new ArticleCommentViewModel(item));

            articleDetailsVm.ArticleComments = articleCommentsVm;
            var articleTags = _repo.GetArticleTags(id);
            articleDetailsVm.Tags = articleTags;
            var articleHeadlines = _repo.GetArticleHeadlines(id);
            articleDetailsVm.HeadLines = articleHeadlines;
            return View(articleDetailsVm);
        }
        [HttpPost]
        public ActionResult PostComment(CommentFormViewModel form)
        {
            if (ModelState.IsValid)
            {
                var comment = new ArticleComment()
                {
                    ArticleId = form.ArticleId,
                    ParentId = form.ParentId,
                    Name = form.Name,
                    Email = form.Email,
                    Message = form.Message,
                    AddedDate = DateTime.Now,
                };
                _repo.AddComment(comment);
                return RedirectToAction("ContactUsSummary", "Home");
            }
            return RedirectToAction("Details",new {id = form.ArticleId});
        }
        public ActionResult LatestPosts(int take = 1)
        {
            var latestArticlesVm = new List<LatestArticleViewModel>();
            var articles = _repo.GetArticles(take);
            foreach (var item in articles)
            {
                latestArticlesVm.Add(new LatestArticleViewModel(item));
            }
            return PartialView(latestArticlesVm);
        }
    }
}
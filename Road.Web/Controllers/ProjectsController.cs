using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Road.Infrastructure.Repositories;
using Road.Web.ViewModels;

namespace Road.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectsRepository _repo;

        public ProjectsController(ProjectsRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index()
        {
            var projects = _repo.GetProjects();
            var projectTypes = projects.DistinctBy(p => p.ProjectTypeId).Select(p => p.ProjectType).ToList();
            var projectSectionVm = new ProjectSectionViewModel()
            {
                ProjectTypes = projectTypes,
                Projects = projects
            };
            #region BreadCrumb
            var breadCrumbVm = new List<BreadCrumbViewModel>();
            breadCrumbVm.Add(new BreadCrumbViewModel() { Title = "پروژه های فرادید", Href = "#" });
            ViewBag.BreadCrumb = breadCrumbVm;
            #endregion
            return View(projectSectionVm);
        }
        [Route("Projects/{id}/{title}")]
        public ActionResult Details(int id)
        {
            var project = _repo.GetProjectDetail(id);
            var projectGallery = _repo.GetProjectGallery(project.Id);
            var similarProjects = _repo.GetProjectsByTypeId(project.ProjectTypeId.Value);

            // Removing current project from similar projects
            if (similarProjects.Any())
                similarProjects.Remove(project);

            var projectDetailVm = new ProjectDetailViewModel()
            {
                Project = project,
                Gallery = projectGallery,
                SimilarProjects = similarProjects
            };
            #region BreadCrumb
            var breadCrumbVm = new List<BreadCrumbViewModel>();
            breadCrumbVm.Add(new BreadCrumbViewModel() { Title = "پروژه های فرادید", Href = "/Projects" });
            breadCrumbVm.Add(new BreadCrumbViewModel() { Title = project.Title, Href = "#" });
            ViewBag.BreadCrumb = breadCrumbVm;
            #endregion
            return View(projectDetailVm);
        }
    }
}
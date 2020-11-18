using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Road.Infrastructure.Repositories;
using Road.Core.Models;
using System.Net;
using System.IO;
using Road.Infrastructure.Helpers;

namespace Road.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectGalleriesController : Controller
    {
        private readonly ProjectGalleriesRepository _repo;
        public ProjectGalleriesController(ProjectGalleriesRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index(int projectId)
        {
            ViewBag.ProjectName = _repo.GetProjectName(projectId);
            ViewBag.ProjectId = projectId;
            return View(_repo.GetProjectGalleries(projectId));
        }

        public ActionResult Create(int projectId)
        {
            ViewBag.ProjectId = projectId;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectGallery projectGallery,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (Image != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(900, 500,true);
                    image.Resize(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName),
                        Server.MapPath("//Files/ProjectImages/ProjectGallery/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));

                    projectGallery.Image = newFileName;
                }
                #endregion
                _repo.Add(projectGallery);
                return RedirectToAction("Index", new { projectId = projectGallery.ProjectId });
            }
            ViewBag.ProjectId = projectGallery.ProjectId;
            return View(projectGallery);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectGallery projectGallery = _repo.Get(id.Value);
            if (projectGallery == null)
            {
                return HttpNotFound();
            }
            return PartialView(projectGallery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectGallery projectGallery,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (Image != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/ProjectImages/ProjectGallery/" + projectGallery.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/ProjectGallery/" + projectGallery.Image));
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(900, 500,true);
                    image.Resize(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName),
                        Server.MapPath("//Files/ProjectImages/ProjectGallery/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));

                    projectGallery.Image = newFileName;
                }
                #endregion
                _repo.Update(projectGallery);
                return RedirectToAction("Index", new { projectId = projectGallery.ProjectId });
            }
            return View(projectGallery);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectGallery projectGallery = _repo.Get(id.Value);
            if (projectGallery == null)
            {
                return HttpNotFound();
            }
            return PartialView(projectGallery);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var projectId = _repo.Get(id).ProjectId;
            _repo.Delete(id);
            return RedirectToAction("Index", new { projectId });
        }
    }
}
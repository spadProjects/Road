using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Road.Core.Models;
using Road.Infrastructure;
using Road.Infrastructure.Helpers;
using Road.Infrastructure.Repositories;
using Road.Web.ViewModels;

namespace Road.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ProjectsRepository _repo;
        public ProjectsController(ProjectsRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/Projects
        public ActionResult Index()
        {
            return View(_repo.GetProjects());
        }
        // GET: Admin/Projects/Create
        public ActionResult Create()
        {
            ViewBag.ProjectTypeId = new SelectList(_repo.GetProjectTypes(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project, HttpPostedFileBase ProjectImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (ProjectImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(ProjectImage.FileName);
                    ProjectImage.SaveAs(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(840, 385,true);
                    image.Resize(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ProjectImages/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer(400, 250,true);
                    thumb.Resize(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ProjectImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));

                    project.Image = newFileName;
                }
                #endregion

                _repo.Add(project);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectTypeId = new SelectList(_repo.GetProjectTypes(), "Id", "Title", project.ProjectTypeId);
            return View(project);
        }

        // GET: Admin/Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _repo.Get(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectTypeId = new SelectList(_repo.GetProjectTypes(), "Id", "Title", project.ProjectTypeId);
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project, HttpPostedFileBase ProjectImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (ProjectImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/ProjectImages/Image/" + project.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Image/" + project.Image));

                    if (System.IO.File.Exists(Server.MapPath("/Files/ProjectImages/Thumb/" + project.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Thumb/" + project.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(ProjectImage.FileName);
                    ProjectImage.SaveAs(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(840, 385,true);
                    image.Resize(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ProjectImages/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer(370, 250,true);
                    thumb.Resize(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ProjectImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));

                    project.Image = newFileName;
                }
                #endregion

                _repo.Update(project);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectTypeId = new SelectList(_repo.GetProjectTypes(), "Id", "Title", project.ProjectTypeId);
            return View(project);
        }

        [HttpPost]
        public ActionResult FileUpload()
        {
            var files = HttpContext.Request.Files;
            foreach (var fileName in files)
            {
                HttpPostedFileBase file = Request.Files[fileName.ToString()];
                var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("/Files/ProjectImages/" + newFileName));
                TempData["UploadedFile"] = newFileName;
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        // GET: Admin/Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _repo.Get(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return PartialView(project);
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = _repo.Get(id);

            //#region Delete Project Image
            //if (project.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/ProjectImages/Image/" + project.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Image/" + project.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/ProjectImages/Thumb/" + project.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Thumb/" + project.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

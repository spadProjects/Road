using System;
using System.Net;
using System.Web.Mvc;
using Road.Core.Models;
using Road.Infrastructure.Repositories;

namespace Road.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectTypesController : Controller
    {
        private readonly ProjectTypesRepository _repo;
        public ProjectTypesController(ProjectTypesRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/ProjectTypes
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Admin/ProjectTypes/Create
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectType projectType)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(projectType);
                return RedirectToAction("Index");
            }

            return View(projectType);
        }

        // GET: Admin/ProjectTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectType projectType = _repo.Get(id.Value);
            if (projectType == null)
            {
                return HttpNotFound();
            }
            return PartialView(projectType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectType projectType)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(projectType);
                return RedirectToAction("Index");
            }
            return View(projectType);
        }

        // GET: Admin/ProjectTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectType projectType = _repo.Get(id.Value);
            if (projectType == null)
            {
                return HttpNotFound();
            }
            return PartialView(projectType);
        }

        // POST: Admin/ProjectTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

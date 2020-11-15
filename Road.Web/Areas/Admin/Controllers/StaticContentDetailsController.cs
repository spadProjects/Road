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
using Road.Core.Utility;
using Road.Infrastructure;
using Road.Infrastructure.Helpers;
using Road.Infrastructure.Repositories;
using Road.Web.ViewModels;

namespace Road.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class StaticContentDetailsController : Controller
    {
        private readonly StaticContentDetailsRepository _repo;
        public StaticContentDetailsController(StaticContentDetailsRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/StaticContentDetails
        public ActionResult Index()
        {
            return View(_repo.GetStaticContentDetails().OrderBy(e=>e.StaticContentTypeId).ToList());
        }
        // GET: Admin/StaticContentDetails/Create
        public ActionResult Create()
        {
            #region AvailableTypes
            var availableTypes = new List<StaticContentType>();
            var slider = _repo.GetStaticContentTypes().FirstOrDefault(a => a.Id == (int)StaticContentTypes.Slider);
            var appreciationLetter = _repo.GetStaticContentTypes().FirstOrDefault(a => a.Id == (int)StaticContentTypes.AppreciationLetters);
            availableTypes.Add(slider);
            availableTypes.Add(appreciationLetter);
            availableTypes.AddRange(_repo.GetStaticContentTypes());
            #endregion
            ViewBag.StaticContentTypeId = new SelectList(availableTypes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaticContentDetail staticContentDetail, HttpPostedFileBase StaticContentDetailImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (StaticContentDetailImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(StaticContentDetailImage.FileName);
                    StaticContentDetailImage.SaveAs(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer image = new ImageResizer();
                    if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.Slider)
                        image = new ImageResizer(1600,860);
                    if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.AppreciationLetters)
                        image = new ImageResizer(370, 215);
                    if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.AboutUs)
                        image = new ImageResizer(770, 570);
                    image.Resize(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName),
                        Server.MapPath("/Files/StaticContentImages/Image/" + newFileName));

                    // Deleting Temp Image
                        System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName));

                    staticContentDetail.Image = newFileName;
                }
                #endregion

                _repo.Add(staticContentDetail);

                return RedirectToAction("Index");
            }
            #region AvailableTypes
            var availableTypes = new List<StaticContentType>();
            var slider = _repo.GetStaticContentTypes().FirstOrDefault(a => a.Id == (int)StaticContentTypes.Slider);
            var appreciationLetter = _repo.GetStaticContentTypes().FirstOrDefault(a => a.Id == (int)StaticContentTypes.AppreciationLetters);
            availableTypes.Add(slider);
            availableTypes.Add(appreciationLetter);
            availableTypes.AddRange(_repo.GetStaticContentTypes());
            #endregion
            ViewBag.StaticContentTypeId = new SelectList(availableTypes, "Id", "Name", staticContentDetail.StaticContentTypeId);
            return View(staticContentDetail);
        }

        // GET: Admin/StaticContentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticContentDetail staticContentDetail = _repo.GetStaticContentDetail(id.Value);
            if (staticContentDetail == null)
            {
                return HttpNotFound();
            }
            //ViewBag.StaticContentTypeId = new SelectList(_repo.GetStaticContentTypes(), "Id", "Name", staticContentDetail.StaticContentTypeId);
            return View(staticContentDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StaticContentDetail staticContentDetail, HttpPostedFileBase StaticContentDetailImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (StaticContentDetailImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/StaticContentImages/Image/" + staticContentDetail.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Image/" + staticContentDetail.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(StaticContentDetailImage.FileName);
                    StaticContentDetailImage.SaveAs(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer image = new ImageResizer();

                    if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.Slider)
                        image = new ImageResizer(1600,860);
                    if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.AppreciationLetters)
                        image = new ImageResizer(370, 215);
                    if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.AboutUs)
                        image = new ImageResizer(770, 570);
                    image.Resize(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName),
                        Server.MapPath("/Files/StaticContentImages/Image/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName));

                    staticContentDetail.Image = newFileName;
                }
                #endregion

                _repo.Update(staticContentDetail);
                return RedirectToAction("Index");
            }
            //ViewBag.StaticContentTypeId = new SelectList(_repo.GetStaticContentTypes(), "Id", "Name", staticContentDetail.StaticContentTypeId);
            return View(staticContentDetail);
        }
        // GET: Admin/StaticContentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticContentDetail staticContentDetail = _repo.GetStaticContentDetail(id.Value);
            if (staticContentDetail == null)
            {
                return HttpNotFound();
            }
            return PartialView(staticContentDetail);
        }

        // POST: Admin/StaticContentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var staticContentDetail = _repo.Get(id);

            //#region Delete StaticContentDetail Image
            //if (staticContentDetail.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/StaticContentImages/Image/" + staticContentDetail.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Image/" + staticContentDetail.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/StaticContentImages/Thumb/" + staticContentDetail.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Thumb/" + staticContentDetail.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

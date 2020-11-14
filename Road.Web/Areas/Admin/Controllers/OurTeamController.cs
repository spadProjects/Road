﻿using System;
using System.Net;
using System.Web.Mvc;
using Road.Core.Models;
using Road.Infrastructure.Repositories;
using System.Web;
using System.IO;

namespace Road.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class OurTeamController : Controller
    {
        private readonly OurTeamRepository _repo;
        public OurTeamController(OurTeamRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OurTeam outTeam, HttpPostedFileBase OurTeamImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (OurTeamImage != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(OurTeamImage.FileName);
                    OurTeamImage.SaveAs(Server.MapPath("/Files/OurTeamImages/" + newFileName));
                    outTeam.Image = newFileName;
                }
                #endregion

                _repo.Add(outTeam);
                return RedirectToAction("Index");
            }

            return View(outTeam);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OurTeam outTeam = _repo.Get(id.Value);
            if (outTeam == null)
            {
                return HttpNotFound();
            }
            return View(outTeam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OurTeam outTeam, HttpPostedFileBase OurTeamImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (OurTeamImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/OurTeamImages/" + outTeam.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/OurTeamImages/" + outTeam.Image));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(OurTeamImage.FileName);
                    OurTeamImage.SaveAs(Server.MapPath("/Files/OurTeamImages/" + newFileName));
                    outTeam.Image = newFileName;
                }
                #endregion

                _repo.Update(outTeam);
                return RedirectToAction("Index");
            }
            return View(outTeam);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OurTeam outTeam = _repo.Get(id.Value);
            if (outTeam == null)
            {
                return HttpNotFound();
            }
            return PartialView(outTeam);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var outTeam = _repo.Get(id);

            //#region Delete Image
            //if (outTeam.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/OurTeamImages/" + outTeam.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/OurTeamImages/" + outTeam.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/OurTeamImages/" + outTeam.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/OurTeamImages/" + outTeam.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
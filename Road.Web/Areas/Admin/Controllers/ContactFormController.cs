﻿using System;
using System.Net;
using System.Web.Mvc;
using Road.Core.Models;
using Road.Infrastructure.Repositories;

namespace Road.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ContactFormController : Controller
    {
        private readonly ContactFormsRepository _repo;
        public ContactFormController(ContactFormsRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }
        public ActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(_repo.GetServices(), "Id", "Title");
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(contactForm);
                return RedirectToAction("Index");
            }
            ViewBag.ServiceId = new SelectList(_repo.GetServices(), "Id", "Title");
            return View(contactForm);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = _repo.Get(id.Value);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = new SelectList(_repo.GetServices(), "Id", "Title",contactForm.ServiceId);
            return PartialView(contactForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(contactForm);
                return RedirectToAction("Index");
            }
            ViewBag.ServiceId = new SelectList(_repo.GetServices(), "Id", "Title", contactForm.ServiceId);
            return View(contactForm);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = _repo.GetContactForm(id.Value);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return PartialView(contactForm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

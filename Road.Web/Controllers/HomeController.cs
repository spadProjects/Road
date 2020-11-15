using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Road.Core.Models;
using Road.Core.Utility;
using Road.Infrastructure.Repositories;
using Road.Web.ViewModels;

namespace Road.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly StaticContentDetailsRepository _contentRepo;
        private readonly FaqRepository _faqRepo;
        private readonly ProjectsRepository _projectRepo;
        private readonly PartnersRepository _partnerRepo;
        private readonly TestimonialsRepository _testimonialRepo;
        public HomeController(StaticContentDetailsRepository contentRepo, FaqRepository faqRepo,ProjectsRepository projectRepo,PartnersRepository partnerRepo,TestimonialsRepository testimonialRepo)
        {
            _contentRepo = contentRepo;
            _faqRepo = faqRepo;
            _projectRepo = projectRepo;
            _partnerRepo = partnerRepo;
            _testimonialRepo = testimonialRepo;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            var headerContent = new List<StaticContentDetail>();
            var socialNetworks = _contentRepo.GetContentByTypeId((int) StaticContentTypes.SocialNetworks);
            headerContent.AddRange(socialNetworks);
            var companyInfo = _contentRepo.GetContentByTypeId((int) StaticContentTypes.CompanyInfo);
            headerContent.AddRange(companyInfo);
            return PartialView(headerContent);
        }
        public ActionResult HomeSlider()
        {
            var sliderContent = _contentRepo.GetContentByTypeId((int)StaticContentTypes.Slider);
            return PartialView(sliderContent);
        }

        public ActionResult AppreciationLetters()
        {
            ViewBag.ContentType = _contentRepo.GetContentType((int)StaticContentTypes.AppreciationLetters).Name;
            var appreciationLetters = _contentRepo.GetContentByTypeId((int)StaticContentTypes.AppreciationLetters);
            return PartialView(appreciationLetters);
        }

        public ActionResult OutstandingConstruction()
        {
            ViewBag.ContentType = _contentRepo.GetContentType((int)StaticContentTypes.OutstandingConstruction).Name;
            var outstandingConstruction = _contentRepo.GetContentByTypeId((int)StaticContentTypes.OutstandingConstruction);
            return PartialView(outstandingConstruction);
        }

        public ActionResult ProjectsSection()
        {
            var projects = _projectRepo.GetProjects();
            var projectTypes = projects.DistinctBy(p=>p.ProjectTypeId).Select(p => p.ProjectType).ToList();
            var projectSectionVm = new ProjectSectionViewModel()
            {
                ProjectTypes = projectTypes,
                Projects = projects
            };
            return PartialView(projectSectionVm);
        }
        public ActionResult FaqSection()
        {
            ViewBag.Content = _contentRepo.GetContentByTypeId((int)StaticContentTypes.Faq).FirstOrDefault();
            var faqs = _faqRepo.GetAll();
            return PartialView(faqs);
        }

        public ActionResult PartnersSection()
        {
            var partners = _partnerRepo.GetAll();
            return PartialView(partners);
        }

        public ActionResult TestimonialsSection()
        {
            ViewBag.Content = _contentRepo.GetContentByTypeId((int) StaticContentTypes.Testimonials).FirstOrDefault();
            var testimonials = _testimonialRepo.GetAll();
            return PartialView(testimonials);
        }
        [Route("AboutUs")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var AboutUs = new List<StaticContentDetail>();
            var aboutUsContent = _contentRepo.GetContentByTypeId((int)StaticContentTypes.AboutUs).FirstOrDefault();
            AboutUs.Add(aboutUsContent);
            var outstandingConstruction = _contentRepo.GetContentByTypeId((int)StaticContentTypes.OutstandingConstruction);
            AboutUs.AddRange(outstandingConstruction);
            var counters = _contentRepo.GetContentByTypeId((int)StaticContentTypes.Counter);
            AboutUs.AddRange(counters);

            return View(AboutUs);
        }
        //[Route("ContactUs")]
        //public ActionResult Contact()
        //{
        //    var ContactUs = new List<StaticContentDetail>();
        //    var contactUsContent = _contentRepo.GetContentByTypeId((int)StaticContentTypes.ContactUs).FirstOrDefault();
        //    ContactUs.Add(contactUsContent);
        //    var companyInfo = _contentRepo.GetContentByTypeId((int)StaticContentTypes.CompanyInfo);
        //    headerContent.AddRange(companyInfo);
        //    return View();
        //}
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                    Path.GetExtension(upload.FileName).ToLower();
                    var vFolderPath = Server.MapPath("/Upload/");
                    if (!Directory.Exists(vFolderPath))
                    {
                        Directory.CreateDirectory(vFolderPath);
                    }
                    vFilePath = Path.Combine(vFolderPath, vFileName);
                    upload.SaveAs(vFilePath);
                    vImagePath = Url.Content("/Upload/" + vFileName);
                    vMessage = "Image was saved correctly";
                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput);
        }
    }
}
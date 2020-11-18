using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Road.Core.Models
{
    public class Project : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "عنوان پروژه")]
        [MaxLength(600, ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیح کوتاه")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیح")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "مشتری")]
        [MaxLength(600, ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        public string Customer { get; set; }
        [Display(Name = "بودجه")]
        [MaxLength(600, ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        public string Budget { get; set; }
        [Display(Name = "منطقه سطح")]
        [MaxLength(600, ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        public string SurfaceArea { get; set; }
        [Display(Name = "تاریخ تکمیل")]
        [MaxLength(600, ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        public string CompletedDate { get; set; }
        [Display(Name = "نخستین مشاور")]
        [MaxLength(600, ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        public string FirstConsultant { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        public int? ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; }

        public ICollection<ProjectGallery> ProjectGalleries { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}

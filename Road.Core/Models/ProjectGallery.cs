using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Road.Core.Models
{
   public class ProjectGallery : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }

        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        public int? ProjectId { get; set; }
        public Project Project { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Road.Core.Models
{
    public class ProjectType : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "نوع پروژه")]
        [MaxLength(600, ErrorMessage = "نوع پروژه باید از 600 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا نوع پروژه را وارد کنید")]
        public string Title { get; set; }
        public ICollection<Project> Projects { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}

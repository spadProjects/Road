using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Road.Core.Models;

namespace Road.Web.ViewModels
{
    public class ProjectSectionViewModel
    {
        public List<ProjectType> ProjectTypes { get; set; }
        public List<Project> Projects { get; set; }
    }
}
using Road.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Road.Infrastructure.Repositories
{
    public class ProjectGalleriesRepository : BaseRepository<ProjectGallery, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ProjectGalleriesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<ProjectGallery> GetProjectGalleries(int projectId)
        {
            return _context.ProjectGalleries.Where(h => h.ProjectId == projectId & h.IsDeleted == false).ToList();
        }
        public string GetProjectName(int projectId)
        {
            return _context.Projects.Find(projectId).Title;
        }
    }
}

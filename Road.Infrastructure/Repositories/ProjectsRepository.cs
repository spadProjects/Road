using Road.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Road.Infrastructure.Repositories
{
    public class ProjectsRepository : BaseRepository<Project, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ProjectsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<Project> GetProjects()
        {
            return _context.Projects.Include(p => p.ProjectType).Where(p => p.IsDeleted == false).OrderByDescending(a => a.InsertDate).ToList();
        }
        public List<ProjectType> GetProjectTypes()
        {
            return _context.ProjectTypes.Where(a => a.IsDeleted == false).ToList();
        }

        public Project GetProjectDetail(int id)
        {
            return _context.Projects.Include(p => p.ProjectType).FirstOrDefault(p => p.IsDeleted == false && p.Id == id);
        }

        public List<ProjectGallery> GetProjectGallery(int id)
        {
            return _context.ProjectGalleries.Where(g => g.ProjectId == id && g.IsDeleted == false).ToList();
        }
        public List<Project> GetProjectsByTypeId(int typeId)
        {
            return _context.Projects.Where(p => p.ProjectTypeId == typeId && p.IsDeleted == false).Include(p=>p.ProjectType).ToList();
        }
    }
}

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
            return _context.Projects.Include(p => p.ProjectType).Where(p => p.IsDeleted == false).OrderBy(a => a.InsertDate).ToList();
        }
        public List<ProjectType> GetProjectTypes()
        {
            return _context.ProjectTypes.Where(a => a.IsDeleted == false).ToList();
        }
    }
}

﻿using Road.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Road.Infrastructure.Repositories
{
    public class ContactFormsRepository : BaseRepository<ContactForm, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ContactFormsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public ContactForm GetContactForm(int id)
        {
            return _context.ContactForms.FirstOrDefault(c=>c.Id == id);
        }
        public List<Service> GetServices()
        {
            return _context.Services.Where(e=>e.IsDeleted == false).ToList();
        }
    }
}

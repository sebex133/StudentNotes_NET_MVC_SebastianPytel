using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentNotes_MVC_SebastianPytel.Areas.Identity.Data;
using StudentNotes_MVC_SebastianPytel.Models;

namespace StudentNotes_MVC_SebastianPytel.Data
{

    public class StudentNotesContext : IdentityDbContext<StudentNotesUser>
    {
        public StudentNotesContext(DbContextOptions<StudentNotesContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<StudentNote> StudentNote { get; set; }
    }
}

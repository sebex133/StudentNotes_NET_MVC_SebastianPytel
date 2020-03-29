using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentNotes_MVC_SebastianPytel.Areas.Identity.Data;
using StudentNotes_MVC_SebastianPytel.Data;

[assembly: HostingStartup(typeof(StudentNotes_MVC_SebastianPytel.Areas.Identity.IdentityHostingStartup))]
namespace StudentNotes_MVC_SebastianPytel.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<StudentNotesContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("StudentNotesContextConnection")));

                services.AddDefaultIdentity<StudentNotesUser>()
                    .AddEntityFrameworkStores<StudentNotesContext>();
            });
        }
    }
}
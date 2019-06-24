using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using okLims.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Models.NumberSequence> NumberSequence { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Laboratory> Laboratory { get; set; }
        public DbSet<Method> Method { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<MethodLine> MethodLine { get; set; }
        public DbSet<RequestLine> RequestLine { get; set; }
    }
}

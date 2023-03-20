using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QardlessAPI.Data.Models;

namespace QardlessAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Changelog> Changelogs { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EndUser> EndUsers { get; set; }
    }
}

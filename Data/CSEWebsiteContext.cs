using Microsoft.EntityFrameworkCore;
using CSE_website.Models;
namespace CSE_website.Data
{
    public class CSEWebsiteContext : DbContext
    {
        public CSEWebsiteContext()
        {
        }

        public CSEWebsiteContext(DbContextOptions<CSEWebsiteContext> options)
            : base(options)
        {
        }

        public DbSet<Account>? Accounts { get; set; } = default!;
        public DbSet<Category>? Categories { get; set; } = default!;
        public DbSet<Department>? Departments { get; set; } = default!;
        public DbSet<FacultyOffice>? FacultyOffices { get; set; } = default!;
        public DbSet<Lecturer>? Lecturers { get; set; } = default!;
        public DbSet<Permission>? Permissions { get; set; } = default!;
        public DbSet<Post>? Posts { get; set; } = default!;
        public DbSet<Subject>? Subjects { get; set; } = default!;
        public DbSet<Partner>? Partners { get; set; } = default!;
        public DbSet<Position>? Positions { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.ChildCategoriesList)
                .WithOne(c => c.CategoryParent)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}

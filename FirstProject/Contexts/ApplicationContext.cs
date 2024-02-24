using FirstProject.Configuration;
using FirstProject.Entities;
using FirstProject.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static FirstProject.Entities.Identity.AppUser;

namespace FirstProject.Contexts
{
    public class ApplicationContext : IdentityDbContext<AppUser, AppRole, string >
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentConfiguration).Assembly); // IEntity type-dan torenmish sisteme elave edir
            modelBuilder.Entity<Student>()
                .HasOne<School>(x=>x.School)
                .WithMany(s=>s.Students)
                .HasForeignKey(x=>x.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

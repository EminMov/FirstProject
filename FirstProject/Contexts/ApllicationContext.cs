using FirstProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Contexts
{
    public class ApllicationContext:DbContext
    {
        public ApllicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>()
                .HasOne<School>(x=>x.School)
                .WithMany(s=>s.Students)
                .HasForeignKey(x=>x.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

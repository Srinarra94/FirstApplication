using Microsoft.EntityFrameworkCore;
using FirstApp.Models;

namespace FirstApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<FirstApp.Models.Student> Student { get; set; } = default!;
        public DbSet<FirstApp.Models.StudentMarks> StudentMarks { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting a primary key in Student model
            modelBuilder.Entity<Student>().HasKey(x => x.Id);
            // configure primary key for Studentmarks Model
            modelBuilder.Entity<StudentMarks>().HasKey(x => x.Id);
            // Configure foreign key relationship
            modelBuilder.Entity<StudentMarks>()
                .HasOne(sm => sm.Student) // Navigation property in Studentmarks
                .WithMany(s => s.StudentMarks) // Navigation property in Student
                .HasForeignKey(sm => sm.StudentId) // Foreign key in Studentmarks
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete

        }

    }
}
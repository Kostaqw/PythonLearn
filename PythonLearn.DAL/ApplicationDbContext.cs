using Microsoft.EntityFrameworkCore;
using PythonLearn.DAL.ModelConfiguration;
using PythonLearn.Domain.Entity;
using System.Security.Cryptography.X509Certificates;

namespace PythonLearn.DAL
{
    public class ApplicationDbContext: DbContext
    {
          
        public ApplicationDbContext()
        {
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }*/
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonComment> LessonComments { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Title> Titles{ get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

    }
}

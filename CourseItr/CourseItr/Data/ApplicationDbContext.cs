using CourseItr.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseItr.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MathTopic>().HasData(
                new MathTopic
                {
                    Id = 1,
                    Name = "Геометрия"
                },
                new MathTopic
                {
                    Id = 2,
                    Name = "Арифметика"
                },
                new MathTopic
                {
                    Id = 3,
                    Name = "Алгебра"
                }
            );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<MathTopic> MathTopics { get; set; }
        public DbSet<MTask> MTasks { get; set; }
        public DbSet<RatingModel> RatingModels { get; set; }
        public DbSet<CourseItr.Models.RatingTask> RatingTask { get; set; }
    }
}

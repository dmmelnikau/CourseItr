using CourseItr.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseItr.Data
{
    public class ApplicationDbContext : IdentityDbContext
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
                     Id = 1, Name = "Геометрия"
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
        public DbSet<MathTask> MathTasks { get; set; } 
        public DbSet<MathTopic> MathTopics { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Api.Models;

namespace ToDoList.Api.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(2000);
            });

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), // Use fixed GUIDs for test data
                    Title = "Complete project documentation",
                    Description = "Write up the API documentation and usage examples",
                    CreationTime = DateTime.Parse("2024-01-01T10:00:00Z"),
                    IsCompleted = false
                },
                new TodoItem
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Title = "Review pull requests",
                    Description = "Review and merge outstanding PRs in the main repository",
                    CreationTime = DateTime.Parse("2024-01-02T14:30:00Z"),
                    IsCompleted = true,
                    CompletionTime = DateTime.Parse("2024-01-03T09:15:00Z")
                },
                new TodoItem
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Title = "Setup test environment",
                    Description = "Configure testing tools and write initial test cases",
                    CreationTime = DateTime.Parse("2024-01-03T11:00:00Z"),
                    IsCompleted = false
                }
            );
        }
    }
}

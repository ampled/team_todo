using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace TeamTodo.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
            {} // Set options in Startup.cs
        
        public DbSet<TodoUser> TodoUsers { get; set; }
        public DbSet<TodoType> TodoTypes { get; set; }
        public DbSet<Todo> Todos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.TodoUser)
                .WithMany(tu => tu.Todos)
                //.HasForeignKey(t => t.TodoUserId) // Does not work when foreign key is optional
                .IsRequired(false);
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.TodoType)
                .WithMany(tt => tt.Todos)
                //.HasForeignKey(t => t.TodoTypeId) // Does not work when foreign key is optional
                .IsRequired(false);
        }
        
    }
}
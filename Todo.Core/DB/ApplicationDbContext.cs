using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Entities;

namespace ToDoList.Core.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.IsCompleted).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GetDate()");
            });
        }
    }
}

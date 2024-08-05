using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Entities
{
    public class KanbanDbContext : DbContext
    {
        private string _connectionString = "Data Source=DESKTOP-NJUV7II\\SQLEXPRESS;Initial Catalog=KanbanDB;Integrated Security=True;Trust Server Certificate=True";
        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Column>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Task>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Task>()
                .Property(t => t.Description)
                .HasMaxLength(2000);

            modelBuilder.Entity<Subtask>()
                .Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(100);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

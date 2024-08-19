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
            modelBuilder.Entity<Board>(entity =>
            {
                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany(b => b.Columns)
                      .WithOne(b => b.Board)
                      .OnDelete(DeleteBehavior.Cascade);
            });
                

            modelBuilder.Entity<Column>(entity =>
            {
                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany(c => c.Tasks)
                      .WithOne(c => c.Column)
                      .OnDelete(DeleteBehavior.Cascade);
            });
                

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(t => t.Title)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(t => t.Description)
                      .HasMaxLength(2000);

                entity.HasMany(t => t.Subtasks)
                      .WithOne(s => s.Task)
                      .HasForeignKey(s => s.TaskId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Subtask>(entity =>
            {
                entity.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(100);

                entity.HasOne(s => s.Task)
                      .WithMany(t => t.Subtasks)
                      .HasForeignKey(s => s.TaskId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
                


                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

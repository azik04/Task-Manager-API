using Microsoft.EntityFrameworkCore;
using TaskManager.DataProvider.Entities;
namespace TaskManager.DataProvider.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasData(new Users
            {
                Email = "Admin@gmail.com",
                FullName = "Admin",
                Password = "Admin123",
                Id = 1,
                Role = Enums.Role.Admin
            });
        });

        modelBuilder.Entity<UserThemes>(entity =>
        {
            entity.HasKey(ut => ut.Id);
            entity.HasOne(ut => ut.User)
                .WithMany(u => u.UserThemes)
                .HasForeignKey(ut => ut.UserId);
            entity.HasOne(ut => ut.Theme)
                .WithMany(t => t.UserThemes)
                .HasForeignKey(ut => ut.ThemeId);
        });

        modelBuilder.Entity<Themes>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasMany(t => t.Tasks)
                .WithOne(t => t.Theme)
                .HasForeignKey(t => t.ThemeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserThemes>(entity =>
        {
            entity.HasKey(ut => ut.Id);

            entity.HasOne(ut => ut.Theme)
                  .WithMany(t => t.UserThemes)
                  .HasForeignKey(ut => ut.ThemeId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ut => ut.User)
                  .WithMany(u => u.UserThemes)
                  .HasForeignKey(ut => ut.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Comments>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasOne(c => c.User)
                  .WithMany(u => u.Comment)
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Files>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.HasOne(f => f.Tasks)
                  .WithMany(t => t.Files)
                  .HasForeignKey(f => f.TaskId);
        });

        modelBuilder.Entity<SubTasks>(entity =>
        {
            entity.HasKey(st => st.Id);
            entity.HasOne(st => st.Task)
                  .WithMany(t => t.CoTasks)
                  .HasForeignKey(st => st.TaskId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(st => st.User)
                  .WithMany(u => u.CoTasks)
                  .HasForeignKey(st => st.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserTasks>()
             .HasKey(ut => new { ut.TaskId, ut.UserId });
        modelBuilder.Entity<UserTasks>()
            .HasOne(ut => ut.Tasks)
            .WithMany(t => t.UserTasks)
            .HasForeignKey(ut => ut.TaskId);

        modelBuilder.Entity<UserTasks>()
            .HasOne(ut => ut.Users)
            .WithMany(u => u.UserTasks)
            .HasForeignKey(ut => ut.UserId);
    }



    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<Files> Files { get; set; }
    public DbSet<Themes> Themes { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Comments> Comments { get; set; }
    public DbSet<SubTasks> SubTasks { get; set; }
    public DbSet<UserThemes> UserThemes { get; set; }
    public DbSet<UserTasks> UserTask { get; set; }

}

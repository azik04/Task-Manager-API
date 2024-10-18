using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed initial data for Users
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasData(new Users
            {
                Email = "Admin@gmail.com",
                UserName = "Admin",
                Password = "Admin123",
                Id = 1,
                Role = Enum.Role.Admin
            });
        });

        // Define Theme-User relationships via UserTheme
        modelBuilder.Entity<UserThemes>(entity =>
        {
            entity.HasKey(ut => new { ut.UserId, ut.ThemeId });
            entity.HasOne(ut => ut.User)
                .WithMany(u => u.UserThemes)
                .HasForeignKey(ut => ut.UserId);
            entity.HasOne(ut => ut.Theme)
                .WithMany(t => t.UserThemes)
                .HasForeignKey(ut => ut.ThemeId);
        });

        // Themes relationship
        modelBuilder.Entity<Themes>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasOne(t => t.Users)
                .WithMany(u => u.Themes)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(t => t.Tasks)
                .WithOne(t => t.Theme)
                .HasForeignKey(t => t.ThemeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Tasks relationship
        modelBuilder.Entity<Tasks>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasOne(t => t.ExecutiveUser)
                  .WithMany(u => u.Tasks)
                  .HasForeignKey(t => t.ExecutiveUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(t => t.UserTasks)
                  .WithOne(ut => ut.Task)
                  .HasForeignKey(ut => ut.TaskId);
        });

        // UserTasks relationship (Ensure users are part of the theme’s users)
        modelBuilder.Entity<UserTasks>(entity =>
        {
            entity.HasKey(ut => new { ut.TaskId, ut.UserId });

            entity.HasOne(ut => ut.Task)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(ut => ut.TaskId);

            entity.HasOne(ut => ut.User)
                .WithMany(u => u.UserTasks)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        });

        // Comments relationship
        modelBuilder.Entity<Comments>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasOne(c => c.User)
                  .WithMany(u => u.Comment)
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Files relationship
        modelBuilder.Entity<Files>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.HasOne(f => f.Task)
                  .WithMany(t => t.Files)
                  .HasForeignKey(f => f.TaskId);
        });

        // SubTasks relationship
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
    }




    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<Files> Files { get; set; }
    public DbSet<Themes> Themes { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Comments> Comments { get; set; }
    public DbSet<UserTasks> UserTasks { get; set; }
    public DbSet<SubTasks> SubTasks { get; set; }
    public DbSet<UserThemes> UserThemes { get; set; }
}

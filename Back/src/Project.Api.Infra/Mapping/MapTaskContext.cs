using Microsoft.EntityFrameworkCore;
using Project.Api.Domain.Entities;
using Project.Api.Infra.Context;

namespace Project.Api.Infra.Mapping
{
    public static class MapTaskContext
    {
        public static void MapTask(this DataContext context, ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>().ToTable("Task");

            modelBuilder.Entity<TaskEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<TaskEntity>().HasAlternateKey(x => x.Uuid);
            modelBuilder.Entity<TaskEntity>().Property(x => x.CreateAt);

            modelBuilder.Entity<TaskEntity>().Property(x => x.Title);
            modelBuilder.Entity<TaskEntity>().Property(x => x.CreateAt);
            modelBuilder.Entity<TaskEntity>().Property(x => x.ExpirationDate);

            modelBuilder.Entity<TaskEntity>().Property(x => x.Status);
            modelBuilder.Entity<TaskEntity>().Property(x => x.AuthorId);

            modelBuilder.Entity<TaskEntity>()
                .HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskEntity>()
                .HasOne(x => x.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
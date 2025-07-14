using Microsoft.EntityFrameworkCore;
using Project.Api.Domain.Entities;
using Project.Api.Infra.Context;

namespace Project.Api.Infra.Mapping
{
    public static class MapTaskAuditContext
    {
        public static void MapTaskAudit(this DataContext context, ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskAuditEntity>().ToTable("TaskAudit");

            modelBuilder.Entity<TaskAuditEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<TaskAuditEntity>().HasAlternateKey(x => x.Uuid);
            modelBuilder.Entity<TaskAuditEntity>().Property(x => x.CreateAt);

            modelBuilder.Entity<TaskAuditEntity>().Property(x => x.Title);
            modelBuilder.Entity<TaskAuditEntity>().Property(x => x.CreateAt);
            modelBuilder.Entity<TaskAuditEntity>().Property(x => x.ExpirationDate);
            modelBuilder.Entity<TaskAuditEntity>().Property(x => x.Date);

            modelBuilder.Entity<TaskAuditEntity>().Property(x => x.Status);
            modelBuilder.Entity<TaskAuditEntity>().Property(x => x.AuthorId);
            modelBuilder.Entity<TaskAuditEntity>().Property(x => x.ProjectId);
            modelBuilder.Entity<TaskAuditEntity>().Property(x => x.TaskId);

            modelBuilder.Entity<TaskAuditEntity>()
                .HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskAuditEntity>()
                .HasOne(x => x.Project)
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskAuditEntity>()
                .HasOne(x => x.Task)
                .WithMany()
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
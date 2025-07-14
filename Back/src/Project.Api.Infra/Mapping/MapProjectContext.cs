using Microsoft.EntityFrameworkCore;
using Project.Api.Domain.Entities;
using Project.Api.Infra.Context;

namespace Project.Api.Infra.Mapping
{
    public static class MapProjectContext
    {
        public static void MapProject(this DataContext context, ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEntity>().ToTable("Project");

            modelBuilder.Entity<ProjectEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<ProjectEntity>().HasAlternateKey(x => x.Uuid);

            modelBuilder.Entity<ProjectEntity>().Property(x => x.Status);
            modelBuilder.Entity<ProjectEntity>().Property(x => x.AuthorId);

            modelBuilder.Entity<ProjectEntity>().Property(x => x.CreateAt);

            modelBuilder.Entity<ProjectEntity>()
                .HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectEntity>()
                .HasMany(x => x.Tasks)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
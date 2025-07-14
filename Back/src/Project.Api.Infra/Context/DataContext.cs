using Microsoft.EntityFrameworkCore;
using Project.Api.Domain.Entities;
using Project.Api.Infra.Mapping;

namespace Project.Api.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskAuditEntity> TasksAudit { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.MapUser(modelBuilder);
            this.MapProject(modelBuilder);
            this.MapTask(modelBuilder);
            this.MapTaskAudit(modelBuilder);
        }
    }
}
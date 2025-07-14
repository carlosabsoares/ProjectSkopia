using Microsoft.EntityFrameworkCore;
using Project.Api.Domain.Entities;
using Project.Api.Infra.Context;

namespace Project.Api.Infra.Mapping
{
    public static class MapUserContext
    {
        public static void MapUser(this DataContext context, ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("User");

            modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasAlternateKey(x => x.Uuid);

            modelBuilder.Entity<UserEntity>().Property(x => x.Name);
            modelBuilder.Entity<UserEntity>().Property(x => x.Role);
            modelBuilder.Entity<UserEntity>().Property(x => x.IsActive);
            modelBuilder.Entity<UserEntity>().Property(x => x.CreateAt);
        }
    }
}
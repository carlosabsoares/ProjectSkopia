using Project.Api.Domain.Repositories;
using Project.Api.Infra.Repositories;

//using Project.Api.Infra.Repositories.Product;

namespace Project.Api.Api.DependencyMap
{
    public static class RepositoryDependencyMap
    {
        public static void RepositoryMap(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICudRepository, CudRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskAuditRepository, TaskAuditRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
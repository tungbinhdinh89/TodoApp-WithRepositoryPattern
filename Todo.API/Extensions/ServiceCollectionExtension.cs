using Microsoft.EntityFrameworkCore;
using Todo.Application.Mappings;
using Todo.Application.Repositories.Todo.Application.Repositories;
using Todo.Application.Services;
using ToDoList.Application.Interfaces;
using ToDoList.Core.DB;

namespace ToDoList.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Not found connection string");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ITodoRepository<>), typeof(ToDoRepository<>));
            services.AddScoped<ToDoServices>();
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}

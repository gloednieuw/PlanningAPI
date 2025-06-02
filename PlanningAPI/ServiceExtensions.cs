using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using PlanningAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PlanningAPI
{
	public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services, ApplicationConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.ConnectionString);
            });

   //         services.AddTransient<IRepository<Person>, PersonRepository>();
   //         services.AddTransient<UserManager<User>>();

			//services.AddTransient((provider) => new PersonService(provider.GetRequiredService<IRepository<Person>>()));
			//services.AddTransient((provider) => new AuthenticationService(
			//	provider.GetRequiredService<UserManager<User>>(),
			//	provider.GetRequiredService<IOptions<ApplicationConfiguration>>())
			//);
		}

        public static void AddSwagger(this IServiceCollection services)
        {
			services.AddSwaggerGen(swagger =>
			{
				//This is to generate the Default UI of Swagger Documentation
				swagger.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Planning API",
					Description = ".NET 8 Planning API"
				});
			});
		}
	}
}

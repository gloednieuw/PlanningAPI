using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using PlanningAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PlanningAPI.Model;
using System;
using PlanningAPI.Service;

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

            services.AddTransient<IRepository<Operator>, OperatorRepository>();
			services.AddTransient<IRepository<Trip>, TripRepository>();
			services.AddTransient<IRepository<UpdateLog>, UpdateLogRepository>();

            services.AddTransient((provider) => new DomainServices(
                provider.GetRequiredService<IRepository<Operator>>(),
                provider.GetRequiredService<IRepository<Trip>>(),
                provider.GetRequiredService<IRepository<UpdateLog>>()
            ));

            services.AddTransient((provider) => new OperatorService(provider.GetRequiredService<IRepository<Operator>>()));
			services.AddTransient((provider) => new UpdateLogService(
                provider.GetRequiredService<IRepository<UpdateLog>>(), 
                provider.GetRequiredService<DomainServices>()
            ));
			
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

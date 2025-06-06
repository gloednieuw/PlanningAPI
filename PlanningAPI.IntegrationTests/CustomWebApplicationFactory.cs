using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlanningAPI.Infrastructure;
using PlanningAPI.Model;
using PlanningAPI.Service;

namespace PlanningAPI.IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing ApplicationDbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using InMemory provider
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("IntegrationTestDb");
                });

                services.AddTransient<IRepository<Operator>, OperatorRepository>();
                services.AddTransient<IRepository<Trip>, TripRepository>();
                services.AddTransient<IRepository<UpdateLog>, UpdateLogRepository>();

                services.AddTransient<IDomainServices, DomainServices>((provider) => new DomainServices(
                    provider.GetRequiredService<IRepository<Operator>>(),
                    provider.GetRequiredService<IRepository<Trip>>(),
                    provider.GetRequiredService<IRepository<UpdateLog>>()
                ));

                services.AddTransient<IOperatorService, OperatorService>((provider) => new OperatorService(provider.GetRequiredService<IRepository<Operator>>()));
                services.AddTransient<IUpdateLogService, UpdateLogService>((provider) => new UpdateLogService(
                    provider.GetRequiredService<IRepository<UpdateLog>>(),
                    provider.GetRequiredService<DomainServices>()
                ));

                // Optionally, seed the database here
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    db.Database.EnsureCreated();

                    // Seed operator with ID 1 if not present
                    if (!db.Operators.Any(o => o.OperatorId == 1))
                    {
                        db.Operators.Add(new Operator(1, "IntegrationTestOperator", "api"));
                        db.SaveChanges();
                    }
                }
            });
        }
    }
}
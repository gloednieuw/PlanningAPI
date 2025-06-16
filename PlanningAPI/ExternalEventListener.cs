using PlanningAPI.Service;
using PlanningAPI.Service.Dto;
using System.Threading.Tasks;

namespace PlanningAPI
{
    public class ExternalEventListener : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ExternalEventListener(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Simulate external service sending a trip update message every 30 seconds
                await Task.Delay(30000, stoppingToken);

                UpdateLogAddDto updateLogAddDto = new UpdateLogAddDto(1, DateTime.Now, Model.UpdateLogStatus.OnTime);

                await HandleEvent(updateLogAddDto);
            }
        }

        private async Task HandleEvent(UpdateLogAddDto updateLogAddDto)
        {
            using var scope = _serviceProvider.CreateScope();
            var updateLogService = scope.ServiceProvider.GetRequiredService<UpdateLogService>();

            try
            {
                var succeed = await updateLogService.AddUpdateLogEntryForTrip(updateLogAddDto);
            }
            catch (ArgumentException ex)
            {
                // todo logging
            }
            
        }
    }
}

using Lib.AspNetCore.ServerSentEvents;
using Newtonsoft.Json;

namespace PcStatusAPI
{
    public class CpuEventsWorker : IHostedService
    {
        private IServerSentEventsService eventsService;
        private CpuStatusData cpuStatusData;
        private Task worker;
        private ILogger logger;

        public CpuEventsWorker(IServerSentEventsService eventsService, CpuStatusData cpuStatusData, ILogger<CpuEventsWorker> logger)
        {
            this.eventsService = eventsService;
            this.cpuStatusData = cpuStatusData;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            worker = Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(1000);

                    await cpuStatusData.UpdateCpuTemperatureAsync();
                    await cpuStatusData.UpdateCpuLoadAsync();
                    await cpuStatusData.UpdateCpuSpeedAsync();

                    var eventData = new
                    {
                        temperature = cpuStatusData.CpuTemperature,
                        load = cpuStatusData.CpuLoad,
                        speed = cpuStatusData.CpuSpeed
                    };

                    logger.LogDebug("Sending: temperature: " + eventData.temperature + " load: " + eventData.load + " speed: " + eventData.speed);

                    string? jsonEventData = JsonConvert.SerializeObject(eventData);

                    await eventsService.SendEventAsync("cpu-status", jsonEventData);
                }
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

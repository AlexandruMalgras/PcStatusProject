using PcStatusAPI.Components;

namespace PcStatusAPI.Azure
{
    public class DataUploaderService : BackgroundService
    {
        private readonly CpuStatus cpuStatus;
        private readonly AzureConfiguration azureConfiguration;

        public DataUploaderService()
        {
            this.cpuStatus = CpuStatus.Instance;
            this.azureConfiguration = AzureConfiguration.Instance;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(3000);

                    string? cpuName = cpuStatus.CpuName;
                    double? cpuTemperature = cpuStatus.CpuTemperature;
                    double? cpuLoad = cpuStatus.CpuLoad;
                    double? cpuSpeed = cpuStatus.CpuSpeed;

                    await azureConfiguration.UploadCpuData(DateTime.Now, cpuName, cpuTemperature, cpuLoad, cpuSpeed);
                }
                catch
                {
                    Console.WriteLine("Failed to upload data to Azure Cosmos DB");
                }
            }
        }
    }
}

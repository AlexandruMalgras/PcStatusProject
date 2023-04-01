using PcStatusAPI.Components;
using Microsoft.Azure.Cosmos;

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
                    string? cpuName = cpuStatus.CpuName;
                    double? cpuTemperature = cpuStatus.CpuTemperature;
                    double? cpuLoad = cpuStatus.CpuLoad;
                    double? cpuSpeed = cpuStatus.CpuSpeed;

                    await this.UploadCpuData(DateTime.Now, cpuName, cpuTemperature, cpuLoad, cpuSpeed);
                    await Task.Delay(5000);
                }
                catch
                {
                    Console.WriteLine("Failed to upload data to Azure Cosmos DB");
                }
            }
        }

        public async Task UploadCpuData(DateTime time, string? cpuName, double? temperature, double? load, double? speed)
        {
            try
            {
                var cpuData = new
                {
                    id = Guid.NewGuid().ToString(),
                    Time = time,
                    CpuName = cpuName,
                    CpuTemperature = temperature,
                    CpuLoad = load,
                    CpuSpeed = speed
                };

                var result = await azureConfiguration.Container.CreateItemAsync(cpuData, new PartitionKey(cpuData.id));
                Console.WriteLine("Uploaded to Azure Cosmos DB: \"ID\": " + cpuData.id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

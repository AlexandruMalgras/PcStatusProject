using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PcStatusAPI.Components;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Xml.Linq;
using System.Text.Json.Nodes;

namespace PcStatusAPI.Azure
{
    public class AzureKeyDataCollectorService : BackgroundService
    {
        private readonly AzureConfiguration azureConfiguration;
        private AzureStatus azureStatus;

        public AzureKeyDataCollectorService()
        {
            this.azureConfiguration = AzureConfiguration.Instance;
            this.azureStatus = AzureStatus.Instance;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await GetMaximumCpuTemperatureAsync();
                await GetMaximumCpuLoadAsync();
                await GetMaximumCpuSpeedAsync();

                await Task.Delay(2000);
            }
        }

        public async Task GetMaximumCpuTemperatureAsync()
        {
            var query = new QueryDefinition("SELECT MAX(c.CpuTemperature) FROM c");
            var iterator = this.azureConfiguration.Container.GetItemQueryIterator<object>(query);

            if (iterator.HasMoreResults)
            {
                var result = await iterator.ReadNextAsync();
                object json = result.FirstOrDefault();

                JObject jObject = JObject.FromObject(json);
                JToken jToken = default;

                jObject.TryGetValue("$1", out jToken);

                double toCompare = double.Parse(jToken.ToString());

                if (toCompare > this.azureStatus.MaxCpuTemperature)
                {
                    this.azureStatus.MaxCpuTemperature = toCompare;
                }
            }
        }

        public async Task GetMaximumCpuLoadAsync()
        {
            var query = new QueryDefinition("SELECT MAX(c.CpuLoad) FROM c");
            var iterator = this.azureConfiguration.Container.GetItemQueryIterator<object>(query);

            if (iterator.HasMoreResults)
            {
                var result = await iterator.ReadNextAsync();
                object json = result.FirstOrDefault();

                JObject jObject = JObject.FromObject(json);
                JToken jToken = default;

                jObject.TryGetValue("$1", out jToken);

                double toCompare = double.Parse(jToken.ToString());

                if (toCompare > this.azureStatus.MaxCpuLoad)
                {
                    this.azureStatus.MaxCpuLoad = toCompare;
                }
            }
        }

        public async Task GetMaximumCpuSpeedAsync()
        {
            var query = new QueryDefinition("SELECT MAX(c.CpuSpeed) FROM c WHERE c.CpuSpeed > 0");
            var iterator = this.azureConfiguration.Container.GetItemQueryIterator<object>(query);

            if (iterator.HasMoreResults)
            {
                var result = await iterator.ReadNextAsync();
                object json = result.FirstOrDefault();

                JObject jObject = JObject.FromObject(json);
                JToken jToken = default;

                jObject.TryGetValue("$1", out jToken);

                double toCompare = double.Parse(jToken.ToString());

                if (toCompare > this.azureStatus.MaxCpuSpeed)
                {
                    this.azureStatus.MaxCpuSpeed = toCompare;
                }
            }
        }
    }
}

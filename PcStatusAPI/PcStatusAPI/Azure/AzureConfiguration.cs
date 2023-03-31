using Microsoft.Azure.Cosmos;

namespace PcStatusAPI.Azure
{
    public class AzureConfiguration
    {
        private static readonly AzureConfiguration instance = new AzureConfiguration();
        private readonly IConfiguration configuration;

        private string cosmosDbConnectionString;
        private string cosmosDbDatabaseName;
        private string cosmosDbContainerName;

        private CosmosClient client;
        private Database database;
        private Container container;

        private static int id = 0;

        private AzureConfiguration()
        {
            var path = "./azuresettings.json";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The file {path} does not exist.");
            }

            this.configuration = new ConfigurationBuilder().AddJsonFile(path).Build();

            this.cosmosDbConnectionString = configuration["AzureConfiguration:CosmosDbConnectionString"];
            this.cosmosDbDatabaseName = configuration["AzureConfiguration:CosmosDbDatabaseName"];
            this.cosmosDbContainerName = configuration["AzureConfiguration:CosmosDbContainerName"];

            this.client = new CosmosClient(this.cosmosDbConnectionString);
            this.database = this.client.GetDatabase(this.cosmosDbDatabaseName);
            this.container = this.database.GetContainer(this.cosmosDbContainerName);
        }

        public static AzureConfiguration Instance
        {
            get { return instance; }
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
                    Temperature = temperature,
                    Load = load,
                    Speed = speed
                };

                var result = await this.container.CreateItemAsync(cpuData, new PartitionKey(cpuData.id));
                Console.WriteLine("Uploaded to Azure Cosmos DB: \"ID\": " + cpuData.id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

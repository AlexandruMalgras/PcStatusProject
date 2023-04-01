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

        private static int id = 0;

        private AzureConfiguration()
        {
            this.configuration = new ConfigurationBuilder().AddJsonFile("./azuresettings.json").Build();

            this.cosmosDbConnectionString = configuration["AzureConfiguration:CosmosDbConnectionString"];
            this.cosmosDbDatabaseName = configuration["AzureConfiguration:CosmosDbDatabaseName"];
            this.cosmosDbContainerName = configuration["AzureConfiguration:CosmosDbContainerName"];

            this.Client = new CosmosClient(this.cosmosDbConnectionString);
            this.Database = this.Client.GetDatabase(this.cosmosDbDatabaseName);
            this.Container = this.Database.GetContainer(this.cosmosDbContainerName);
        }

        public static AzureConfiguration Instance
        {
            get { return instance; }
        }

        public CosmosClient Client { get; }
        public Database Database { get; }
        public Container Container { get; }
    }
}

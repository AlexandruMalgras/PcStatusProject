# Computer Status V 1.2

Computer Status is a project that retrieves hardware data from the computer and displays it on a website. The project is built using C#, .Net Core, Azure Cosmos DB, REST API, HTML, CSS, and JavaScript. 

## API Controller

The `StatusController` is an API controller that handles HTTP requests and responses related to the computer's status data. It includes four HTTP GET methods, each of which returns a different piece of information about the computer's CPU:

- `GetCpuName`: Retrieves the name of the computer's CPU.
- `GetCpuTemperature`: Retrieves the current temperature of the computer's CPU.
- `GetCpuLoad`: Retrieves the current load on the computer's CPU. It includes a while loop that waits until the load value is not 0 before returning the result.
- `GetCpuSpeed`: Retrieves the current speed of the computer's CPU.

Each method calls a synchronous method to update the corresponding property in the `CpuStatus` instance. Once the update is complete, the method returns an `Ok` response with the requested data in a JSON object.

The `AzureKeyDataController` is an API controller that handles HTTP requests and responses related to the maximum values of CPU temperature, CPU load, and CPU speed data stored in Azure Cosmos DB. It includes three HTTP GET methods:

- `GetMaxCpuTemperature`: Retrieves the maximum recorded CPU temperature from Azure Cosmos DB.
- `GetMaxCpuLoad`: Retrieves the maximum recorded CPU load from Azure Cosmos DB.
- `GetMaxCpuSpeed`: Retrieves the maximum recorded CPU speed from Azure Cosmos DB.

Each method calls a synchronous method to retrieve the corresponding data from the `AzureStatus` instance. Once the data is retrieved, the method returns an `Ok` response with the requested data in a JSON object.

# Computer Status V 1.2

Computer Status is a project that retrieves hardware data from the computer and displays it on a website. The project is built using C#, .Net Core, Azure Cosmos DB, REST API, HTML, CSS, and JavaScript. 

## Installation

To install this application, follow these steps:

1. Pull/download the files from https://github.com/AlexandruMalgras/PcStatusProject.
2. Navigate within the project to `\PcStatusProject\PcStatusAPI\PcStatusAPI\bin\Release\net7.0`.
3. Start the `PcStatusApi.exe` file to run the server.
4. Open the `PcStatusFrontend.html` file located at `\PcStatusProject` to see the data in real time.

Note: The `PcStatusFrontend.html` file requires an internet connection to display the data.

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

## Future Plans
I have several plans to expand the project in the future. Currently, the application only retrieves data from the computer's CPU, but I plan to include more hardware components in the future.

Additionally, I plan to upload the data to Azure for analysis and to retrieve key data into the application using SQL. This will allow me to track important metrics, such as the highest temperature recorded, and analyze trends over time.

Furthermore, I plan to record more in-depth data separately on the website. For example, I plan to display details for each core of the CPU, so that users can better understand their computer's performance.
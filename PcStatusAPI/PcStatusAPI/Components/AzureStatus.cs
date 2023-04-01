namespace PcStatusAPI.Components
{
    public class AzureStatus
    {
        private static readonly AzureStatus instance = new AzureStatus();

        private AzureStatus()
        {
            MaxCpuTemperature = 0;
            MaxCpuLoad = 0;
            MaxCpuSpeed = 0;
        }

        public static AzureStatus Instance { get { return instance; } }

        public double MaxCpuTemperature { get; set; }
        public double MaxCpuLoad { get; set; }
        public double MaxCpuSpeed { get; set; }
    }
}

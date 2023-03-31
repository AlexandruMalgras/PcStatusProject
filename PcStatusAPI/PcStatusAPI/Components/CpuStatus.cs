namespace PcStatusAPI.Components
{
    public class CpuStatus
    {
        private static readonly CpuStatus instance = new CpuStatus();

        private CpuStatus() { }

        public static CpuStatus Instance
        {
            get { return instance; }
        }

        public double? CpuTemperature { get; set; }
        public double? CpuLoad { get; set; }
        public double? CpuSpeed { get; set; }
        public string? CpuName { get; set; }
    }
}

using OpenHardwareMonitor.Hardware;
using System.Linq;
using System.Management;

namespace PcStatusAPI
{
    public class CpuStatusData
    {
        private Computer computer;
        private SemaphoreSlim semaphore;

        public double? CpuTemperature { get; set; }
        public double? CpuLoad { get; set; }
        public double? CpuSpeed { get; set; }
        public string CpuName { get; set; }

        public CpuStatusData() 
        {
            this.computer = new Computer();
            this.computer.CPUEnabled = true;

            semaphore = new SemaphoreSlim(1, 1);
        }

        public async Task UpdateCpuTemperatureAsync()
        {
            await semaphore.WaitAsync();
            try
            {
                this.computer.Open();

                IHardware? cpu = computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.CPU);

                if (cpu != null)
                {
                    cpu.Update();

                    ISensor? temperatureSensor = cpu.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature);

                    this.CpuTemperature = (temperatureSensor?.Value - 32) * 5 / 9;
                    this.CpuTemperature = double.Parse(CpuTemperature?.ToString(".0"));
                }
            }
            finally
            {
                this.computer.Close();
                this.semaphore.Release();
            }
        }

        public async Task UpdateCpuLoadAsync()
        {
            await this.semaphore.WaitAsync();
            try
            {
                this.computer.Open();

                IHardware? cpu = computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.CPU);

                if (cpu != null)
                {
                    cpu.Update();

                    ISensor loadSensor = cpu.Sensors.LastOrDefault(h => h.SensorType == SensorType.Load);

                    this.CpuLoad = double.Parse(loadSensor.Value?.ToString(".0"));
                }
            }
            finally
            {
                this.computer.Close();
                this.semaphore.Release();
            }
        }

        public async Task UpdateCpuSpeedAsync()
        {
            await this.semaphore.WaitAsync();
            try
            {
                this.computer.Open();

                IHardware? cpu = computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.CPU);

                if (cpu != null)
                {
                    cpu.Update();

                    ISensor[] speedSensors = cpu.Sensors.Where(h => h.SensorType == SensorType.Clock).ToArray();

                    this.CpuSpeed = 0;

                    for (int i = 0; i < speedSensors.Length; i++)
                    {
                        this.CpuSpeed += speedSensors[i].Value;
                    }

                    this.CpuSpeed = CpuSpeed / 8 / 1000;
                    this.CpuSpeed = double.Parse(CpuSpeed?.ToString(".00"));
                }
            }
            finally
            {
                this.computer.Close();
                this.semaphore.Release();
            }
        }

        public async Task UpdateCpuNameAsync()
        {
            await semaphore.WaitAsync();
            try
            {
                this.computer.Open();

                IHardware? cpu = computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.CPU);

                if (cpu != null)
                {
                    this.CpuName = cpu.Name;
                }
            }
            finally
            {
                this.computer.Close();
                this.semaphore.Release();
            }
        }
    }
}

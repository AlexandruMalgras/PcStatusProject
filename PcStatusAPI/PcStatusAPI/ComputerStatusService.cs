using OpenHardwareMonitor.Hardware;
using PcStatusAPI.Components;
using System.Linq;
using System.Management;

namespace PcStatusAPI
{
    public class ComputerStatusService : BackgroundService
    {
        private Computer computer;
        private SemaphoreSlim semaphore;
        private CpuStatus cpuStatus;

        public ComputerStatusService() 
        {
            this.computer = new Computer();
            this.computer.CPUEnabled = true;

            semaphore = new SemaphoreSlim(1, 1);

            cpuStatus = CpuStatus.Instance;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);

                await this.UpdateCpuNameAsync();
                await this.UpdateCpuTemperatureAsync();
                await this.UpdateCpuLoadAsync();

                while (cpuStatus.CpuLoad == 0)
                {
                    await this.UpdateCpuLoadAsync();
                }

                await this.UpdateCpuSpeedAsync();
            }
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

                    this.cpuStatus.CpuTemperature = (temperatureSensor?.Value - 32) * 5 / 9;
                    this.cpuStatus.CpuTemperature = double.Parse(this.cpuStatus.CpuTemperature?.ToString(".0"));
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

                    ISensor? loadSensor = cpu.Sensors.LastOrDefault(h => h.SensorType == SensorType.Load);

                    this.cpuStatus.CpuLoad = double.Parse(loadSensor.Value?.ToString(".0"));
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

                    this.cpuStatus.CpuSpeed = 0;

                    for (int i = 0; i < speedSensors.Length; i++)
                    {
                        this.cpuStatus.CpuSpeed += speedSensors[i].Value;
                    }

                    this.cpuStatus.CpuSpeed = this.cpuStatus.CpuSpeed / 8 / 1000;
                    this.cpuStatus.CpuSpeed = double.Parse(this.cpuStatus.CpuSpeed?.ToString(".00"));
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
                    this.cpuStatus.CpuName = cpu.Name;
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

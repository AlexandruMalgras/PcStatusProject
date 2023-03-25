using OpenHardwareMonitor.Hardware;
using System.Linq;
using System.Management;

namespace PcStatusAPI
{
    public class CpuStatusData
    {
        public double? CpuTemperature { get; set; }
        public double? CpuLoad { get; set; }
        public double? CpuSpeed { get; set; }

        private Computer computer;

        public CpuStatusData() 
        {
            computer = new Computer();
            computer.CPUEnabled = true;
        }

        public async Task UpdateCpuTemperatureAsync()
        {
            await Task.Run(() =>
            {
                computer.Open();

                IHardware? cpu = computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.CPU);

                if (cpu != null)
                {
                    cpu.Update();

                    ISensor? temperatureSensor = cpu.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature);

                    CpuTemperature = (temperatureSensor?.Value - 32) * 5 / 9;
                    CpuTemperature = double.Parse(CpuTemperature?.ToString(".0"));
                }

                computer.Close();
            });
        }

        public async Task UpdateCpuLoadAsync()
        {
            await Task.Run(() =>
            {
                computer.Open();

                IHardware? cpu = computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.CPU);

                if (cpu != null)
                {
                    cpu.Update();

                    ISensor loadSensor = cpu.Sensors.LastOrDefault(h => h.SensorType == SensorType.Load);

                    CpuLoad = double.Parse(loadSensor.Value?.ToString(".0"));
                }

                computer.Close();
            });
        }

        public async Task UpdateCpuSpeedAsync()
        {
            await Task.Run(() =>
            {
                computer.Open();

                IHardware? cpu = computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.CPU);

                if (cpu != null)
                {
                    cpu.Update();

                    ISensor[] speedSensors = cpu.Sensors.Where(h => h.SensorType == SensorType.Clock).ToArray();

                    CpuSpeed = 0;

                    for (int i = 0; i < speedSensors.Length; i++)
                    {
                        CpuSpeed += speedSensors[i].Value;
                    }

                    CpuSpeed = CpuSpeed / 8 / 1000;
                }

                computer.Close();
            });
        }
    }
}

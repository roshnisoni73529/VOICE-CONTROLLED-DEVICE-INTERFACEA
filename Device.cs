using System;

namespace SmartHomeVoiceControl
{
    public enum DeviceType
    {
        Light,
        Fan,
        AirConditioner,
        Speaker
    }

    public class Device
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DeviceType Type { get; set; }
        public bool IsOn { get; set; }
        public int Value { get; set; } // For brightness, speed, temperature, volume
        public string Location { get; set; } = string.Empty;

        public Device(string id, string name, DeviceType type, string location, bool isOn = false, int value = 0)
        {
            Id = id;
            Name = name;
            Type = type;
            Location = location;
            IsOn = isOn;
            Value = value;
        }

        public string GetStatusText()
        {
            if (!IsOn) return "OFF";
            
            return Type switch
            {
                DeviceType.Light => $"ON - {Value}%",
                DeviceType.Fan => $"ON - Speed {Value}%",
                DeviceType.AirConditioner => $"ON - {Value}°C",
                DeviceType.Speaker => $"ON - Volume {Value}%",
                _ => "ON"
            };
        }

        public string GetDisplayName()
        {
            return $"{Location} {Name}";
        }
    }
}
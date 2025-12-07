using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHomeVoiceControl
{
    public class VoiceCommandProcessor
    {
        private readonly List<Device> devices;

        public VoiceCommandProcessor(List<Device> devices)
        {
            this.devices = devices;
        }

        public string ProcessCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return "No command received.";

            command = command.ToLower().Trim();
            
            // Find device mentioned in command
            Device? targetDevice = FindDeviceInCommand(command);
            
            if (targetDevice == null)
                return "Device not found. Please specify a valid device.";

            // Process the command
            if (command.Contains("turn on") || command.Contains("switch on") || command.Contains("start"))
            {
                targetDevice.IsOn = true;
                if (targetDevice.Value == 0)
                {
                    targetDevice.Value = targetDevice.Type switch
                    {
                        DeviceType.Light => 100,
                        DeviceType.Fan => 50,
                        DeviceType.AirConditioner => 22,
                        DeviceType.Speaker => 50,
                        _ => 50
                    };
                }
                return $"{targetDevice.GetDisplayName()} turned on.";
            }
            else if (command.Contains("turn off") || command.Contains("switch off") || command.Contains("stop"))
            {
                targetDevice.IsOn = false;
                return $"{targetDevice.GetDisplayName()} turned off.";
            }
            else if (command.Contains("increase") || command.Contains("up") || command.Contains("higher"))
            {
                targetDevice.IsOn = true;
                int increment = targetDevice.Type == DeviceType.AirConditioner ? 2 : 10;
                int maxValue = targetDevice.Type == DeviceType.AirConditioner ? 30 : 100;
                targetDevice.Value = Math.Min(targetDevice.Value + increment, maxValue);
                return $"{targetDevice.GetDisplayName()} increased to {targetDevice.Value}{GetUnit(targetDevice.Type)}.";
            }
            else if (command.Contains("decrease") || command.Contains("down") || command.Contains("lower"))
            {
                int decrement = targetDevice.Type == DeviceType.AirConditioner ? 2 : 10;
                int minValue = targetDevice.Type == DeviceType.AirConditioner ? 16 : 0;
                targetDevice.Value = Math.Max(targetDevice.Value - decrement, minValue);
                if (targetDevice.Value == 0 && targetDevice.Type != DeviceType.AirConditioner)
                    targetDevice.IsOn = false;
                return $"{targetDevice.GetDisplayName()} decreased to {targetDevice.Value}{GetUnit(targetDevice.Type)}.";
            }
            else if (command.Contains("set") && (command.Contains("to") || command.Contains("at")))
            {
                // Extract number from command
                var words = command.Split(' ');
                for (int i = 0; i < words.Length - 1; i++)
                {
                    if ((words[i] == "to" || words[i] == "at") && int.TryParse(words[i + 1], out int value))
                    {
                        int minValue = targetDevice.Type == DeviceType.AirConditioner ? 16 : 0;
                        int maxValue = targetDevice.Type == DeviceType.AirConditioner ? 30 : 100;
                        targetDevice.Value = Math.Max(minValue, Math.Min(value, maxValue));
                        targetDevice.IsOn = targetDevice.Value > 0 || targetDevice.Type == DeviceType.AirConditioner;
                        return $"{targetDevice.GetDisplayName()} set to {targetDevice.Value}{GetUnit(targetDevice.Type)}.";
                    }
                }
            }

            return $"Command not understood for {targetDevice.GetDisplayName()}.";
        }

        private Device? FindDeviceInCommand(string command)
        {
            // First try to find by full display name
            foreach (var device in devices)
            {
                if (command.Contains(device.GetDisplayName().ToLower()))
                    return device;
            }

            // Then try by location + type
            foreach (var device in devices)
            {
                string locationAndType = $"{device.Location} {device.Type}".ToLower();
                if (command.Contains(locationAndType))
                    return device;
            }

            // Then try by name only
            foreach (var device in devices)
            {
                if (command.Contains(device.Name.ToLower()))
                    return device;
            }

            // Finally try by type only (will return first match)
            foreach (var device in devices)
            {
                if (command.Contains(device.Type.ToString().ToLower()))
                    return device;
            }

            return null;
        }

        private string GetUnit(DeviceType type)
        {
            return type switch
            {
                DeviceType.AirConditioner => "°C",
                _ => "%"
            };
        }
    }
}
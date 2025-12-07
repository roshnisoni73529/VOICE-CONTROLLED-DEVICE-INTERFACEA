# Smart Home Voice Control System (.NET Windows Forms)

A sophisticated Windows desktop application for controlling smart home devices using voice commands and manual controls.

## Features

### 🎤 Voice Recognition
- Real-time speech recognition using Windows Speech API
- Natural language command processing
- Audio feedback with text-to-speech responses
- High confidence threshold for accurate recognition

### 🏠 Device Management
- Support for multiple device types:
  - **Lights** - On/Off control with brightness levels
  - **Fans** - Speed control (0-100%)
  - **Air Conditioners** - Temperature control (16-30°C)
  - **Speakers** - Volume control (0-100%)
- Real-time device status display
- Manual device control via double-click

### 🗣️ Voice Commands
- **Power Control**: "Turn on living room light", "Turn off bedroom fan"
- **Adjustment**: "Increase fan speed", "Decrease volume", "Set temperature to 24"
- **Natural Language**: Supports various phrasings and device references

### 🖥️ User Interface
- Modern Windows Forms design with professional styling
- Real-time command display and system responses
- Device status grid with color-coded indicators
- Manual testing interface for command validation

## System Requirements

- **OS**: Windows 10/11
- **Framework**: .NET 8.0
- **Audio**: Microphone for voice input, speakers for audio feedback
- **Memory**: 100MB RAM minimum

## Installation & Setup

1. **Prerequisites**:
   - Install .NET 8.0 Runtime
   - Ensure microphone permissions are enabled

2. **Build & Run**:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

3. **First Launch**:
   - Click "Start Listening" to begin voice recognition
   - Test commands using the manual input field
   - Double-click devices in the list to toggle manually

## Voice Command Examples

### Basic Control
- "Turn on living room light"
- "Switch off ceiling fan"
- "Start smart speaker"

### Adjustments
- "Increase bedroom light brightness"
- "Set air conditioner to 22 degrees"
- "Turn up speaker volume"
- "Decrease fan speed"

### Specific Values
- "Set living room light to 75 percent"
- "Set temperature to 24"
- "Set volume to 60"

## Architecture

### Core Components
- **MainForm.cs**: Primary UI and application logic
- **Device.cs**: Device model with properties and status methods
- **VoiceCommandProcessor.cs**: Natural language processing engine
- **Program.cs**: Application entry point

### Speech Integration
- **Recognition**: Windows Speech Recognition Engine
- **Synthesis**: Text-to-speech for system responses
- **Grammar**: Dynamic grammar generation for device names

### Device Management
- **State Management**: Real-time device status tracking
- **Command Mapping**: Intelligent device identification from voice input
- **Visual Feedback**: Color-coded status indicators

## Customization

### Adding New Devices
```csharp
devices.Add(new Device("id", "Device Name", DeviceType.Light, "Location", false, 0));
```

### Modifying Voice Commands
Edit the grammar builder in `InitializeSpeech()` method to add new command patterns.

### Styling
Modify colors and fonts in `MainForm.Designer.cs` for custom appearance.

## Troubleshooting

### Speech Recognition Issues
- Ensure microphone is working and permissions are granted
- Check Windows Speech Recognition is enabled in system settings
- Speak clearly and at moderate pace

### Device Control Problems
- Verify device names match voice commands
- Use manual test feature to validate command processing
- Check system response messages for feedback

## Future Enhancements

- **Hardware Integration**: Serial/USB communication with actual devices
- **Network Control**: WiFi/Bluetooth device connectivity
- **Scheduling**: Timer-based device automation
- **Profiles**: User-specific device configurations
- **Mobile App**: Companion mobile application

## Technical Notes

- Uses Windows Speech Platform for voice recognition
- Implements confidence-based command filtering
- Thread-safe device state management
- Extensible architecture for new device types

---

**Developed with .NET 8.0 Windows Forms**  
*Professional smart home automation solution*
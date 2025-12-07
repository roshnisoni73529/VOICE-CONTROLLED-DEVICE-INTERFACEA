# VOICE-CONTROLLED-DEVICE-INTERFACEA
C# Windows Forms based offline voice-controlled device management system with serial communication to microcontrollers. Supports predefined voice commands, GUI controls, and real-time device feedback.




## **Overview**

The **Voice-Controlled Device Management System** is a Windows desktop application that allows users to control external devices (such as Arduino-based circuits) through **offline voice commands**.

It is designed for:

* Students
* Hobbyists
* Educators
* Prototype developers
* Accessibility solutions

This system works **fully offline**, uses **C# (Windows Forms)**, and communicates with hardware using the **Serial Port (COM)** interface.



##  **Key Features**

✔ **Offline Speech Recognition** using `System.Speech.Recognition`
✔ **C# Windows Forms GUI** for easy interaction
✔ **Serial communication** with Arduino or similar devices
✔ **Predefined voice commands** (e.g., “Turn on light”)
✔ **Real-time device status display**
✔ **Lightweight & beginner-friendly**
✔ **No internet required**
✔ **Fully customizable command list**



##  **Tech Stack**

| Component            | Technology                     |
| -------------------- | ------------------------------ |
| Programming Language | C#                             |
| Framework            | .NET Framework / Windows Forms |
| Speech Engine        | System.Speech.Recognition      |
| Hardware Interface   | SerialPort (COM Port)          |
| External Device      | Arduino / Microcontroller      |



##  **Project Structure**

``
/Voice-Controlled-Device-Management
│── /src
│   ├── MainForm.cs
│   ├── SpeechModule.cs
│   ├── SerialCommunication.cs
│   └── Program.cs
│
│── /docs
│   └── Report.pdf (optional)
│
│── README.md
│── LICENSE
│── .gitignore
```

---

## 🚀 **How It Works**

1. User speaks a command (e.g., “Turn on fan”)
2. Speech recognizer converts voice → text (offline)
3. Command processor matches the text with predefined commands
4. Serial command is sent to Arduino (e.g., `FAN_ON`)
5. Arduino performs the required action and sends feedback
6. GUI updates device status in real time


##  **Predefined Commands**

| Voice Command    | Action            |
| ---------------- | ----------------- |
| “Turn on light”  | Sends `LIGHT_ON`  |
| “Turn off light” | Sends `LIGHT_OFF` |
| “Turn on fan”    | Sends `FAN_ON`    |
| “Turn off fan”   | Sends `FAN_OFF`   |

*(You can add more commands in `SpeechModule.cs`.)*



## **Hardware Setup**

* Microcontroller (Arduino recommended)
* USB cable
* LED / Relay / Motor
* Install Arduino IDE
* Upload a simple sketch to read serial commands and control devices

Example Arduino Command Handler:

if (Serial.available()) {
    String cmd = Serial.readStringUntil('\n');

    if (cmd == "LIGHT_ON") digitalWrite(LED, HIGH);
    if (cmd == "LIGHT_OFF") digitalWrite(LED, LOW);
}
```

---

##  **System Requirements**

* Windows 7 / 8 / 10 / 11
* .NET Framework 4.7+
* Microphone
* COM Port (for Arduino)

---

##  **Screenshots** *(Optional – You can add later)*

<img width="525" height="291" alt="image" src="https://github.com/user-attachments/assets/5afd5684-efbd-4843-bd24-9302ad06a215" />









##  **Testing**

* Unit tests for speech recognition
* Serial communication testing using Arduino Serial Monitor
* Real-time testing with hardware
* Peer feedback collection
  
##  **Future Enhancements**

🔹 Add multiple device support
🔹 Add voice feedback
🔹 Add database for saving logs
🔹 Improve UI layout
🔹 Add custom command creation

##  **Author**
**Roshni Soni**
B.E. Computer Science Engineering
Chandigarh University





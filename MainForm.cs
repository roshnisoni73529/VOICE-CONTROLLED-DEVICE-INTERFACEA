using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace SmartHomeVoiceControl
{
    public partial class MainForm : Form
    {
        private SpeechRecognitionEngine? recognitionEngine;
        private SpeechSynthesizer speechSynthesizer;
        private VoiceCommandProcessor commandProcessor;
        private List<Device> devices;
        private bool isListening = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeDevices();
            InitializeSpeech();
            UpdateDeviceDisplay();
        }

        private void InitializeDevices()
        {
            devices = new List<Device>
            {
                new Device("light1", "Light", DeviceType.Light, "Living Room", false, 0),
                new Device("light2", "Light", DeviceType.Light, "Bedroom", true, 75),
                new Device("fan1", "Fan", DeviceType.Fan, "Ceiling", false, 0),
                new Device("ac1", "Air Conditioner", DeviceType.AirConditioner, "Main", false, 22),
                new Device("speaker1", "Speaker", DeviceType.Speaker, "Smart", true, 50)
            };

            commandProcessor = new VoiceCommandProcessor(devices);
        }

        private void InitializeSpeech()
        {
            try
            {
                speechSynthesizer = new SpeechSynthesizer();
                speechSynthesizer.SetOutputToDefaultAudioDevice();

                recognitionEngine = new SpeechRecognitionEngine();
                
                // Create grammar for voice commands
                Choices deviceNames = new Choices();
                foreach (var device in devices)
                {
                    deviceNames.Add(device.GetDisplayName().ToLower());
                    deviceNames.Add(device.Name.ToLower());
                    deviceNames.Add(device.Type.ToString().ToLower());
                    deviceNames.Add($"{device.Location} {device.Type}".ToLower());
                }

                Choices actions = new Choices("turn on", "turn off", "switch on", "switch off", 
                                            "increase", "decrease", "up", "down", "start", "stop",
                                            "set to", "set at", "higher", "lower");

                Choices numbers = new Choices();
                for (int i = 0; i <= 100; i++)
                {
                    numbers.Add(i.ToString());
                }

                GrammarBuilder grammarBuilder = new GrammarBuilder();
                grammarBuilder.Append(actions);
                grammarBuilder.Append(deviceNames);
                grammarBuilder.Append(numbers, 0, 1); // Optional number

                Grammar grammar = new Grammar(grammarBuilder);
                recognitionEngine.LoadGrammar(grammar);

                recognitionEngine.SpeechRecognized += RecognitionEngine_SpeechRecognized;
                recognitionEngine.SpeechRecognitionRejected += RecognitionEngine_SpeechRecognitionRejected;
                recognitionEngine.SetInputToDefaultAudioDevice();

                lblStatus.Text = "Speech recognition initialized successfully.";
                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Speech initialization failed: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void RecognitionEngine_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence > 0.6)
            {
                string command = e.Result.Text;
                txtLastCommand.Text = command;
                
                string response = commandProcessor.ProcessCommand(command);
                txtResponse.Text = response;
                
                // Speak the response
                speechSynthesizer.SpeakAsync(response);
                
                UpdateDeviceDisplay();
            }
        }

        private void RecognitionEngine_SpeechRecognitionRejected(object? sender, SpeechRecognitionRejectedEventArgs e)
        {
            txtResponse.Text = "Command not recognized. Please try again.";
        }

        private void UpdateDeviceDisplay()
        {
            listViewDevices.Items.Clear();
            
            foreach (var device in devices)
            {
                ListViewItem item = new ListViewItem(device.GetDisplayName());
                item.SubItems.Add(device.Type.ToString());
                item.SubItems.Add(device.GetStatusText());
                item.Tag = device;
                
                // Color coding based on status
                if (device.IsOn)
                {
                    item.BackColor = Color.LightGreen;
                }
                else
                {
                    item.BackColor = Color.LightGray;
                }
                
                listViewDevices.Items.Add(item);
            }

            // Update summary
            int activeDevices = devices.Count(d => d.IsOn);
            lblDeviceCount.Text = $"{activeDevices} of {devices.Count} devices active";
        }

        private void btnStartListening_Click(object sender, EventArgs e)
        {
            if (!isListening)
            {
                try
                {
                    recognitionEngine?.RecognizeAsync(RecognizeMode.Multiple);
                    isListening = true;
                    btnStartListening.Text = "Stop Listening";
                    btnStartListening.BackColor = Color.Red;
                    lblStatus.Text = "Listening for voice commands...";
                    lblStatus.ForeColor = Color.Blue;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = $"Error starting recognition: {ex.Message}";
                    lblStatus.ForeColor = Color.Red;
                }
            }
            else
            {
                recognitionEngine?.RecognizeAsyncStop();
                isListening = false;
                btnStartListening.Text = "Start Listening";
                btnStartListening.BackColor = Color.Green;
                lblStatus.Text = "Voice recognition stopped.";
                lblStatus.ForeColor = Color.Black;
            }
        }

        private void btnTestCommand_Click(object sender, EventArgs e)
        {
            string command = txtTestCommand.Text.Trim();
            if (!string.IsNullOrEmpty(command))
            {
                txtLastCommand.Text = command;
                string response = commandProcessor.ProcessCommand(command);
                txtResponse.Text = response;
                speechSynthesizer.SpeakAsync(response);
                UpdateDeviceDisplay();
            }
        }

        private void listViewDevices_DoubleClick(object sender, EventArgs e)
        {
            if (listViewDevices.SelectedItems.Count > 0)
            {
                Device device = (Device)listViewDevices.SelectedItems[0].Tag;
                device.IsOn = !device.IsOn;
                UpdateDeviceDisplay();
                
                string status = device.IsOn ? "turned on" : "turned off";
                txtResponse.Text = $"{device.GetDisplayName()} {status} manually.";
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            recognitionEngine?.Dispose();
            speechSynthesizer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
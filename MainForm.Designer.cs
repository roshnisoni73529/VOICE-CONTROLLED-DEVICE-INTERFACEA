namespace SmartHomeVoiceControl
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnStartListening;
        private Button btnTestCommand;
        private TextBox txtTestCommand;
        private TextBox txtLastCommand;
        private TextBox txtResponse;
        private ListView listViewDevices;
        private Label lblStatus;
        private Label lblDeviceCount;
        private Label lblTitle;
        private Label lblLastCommand;
        private Label lblResponse;
        private Label lblTestCommand;
        private GroupBox groupBoxVoiceControl;
        private GroupBox groupBoxDevices;
        private GroupBox groupBoxManualTest;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnStartListening = new Button();
            this.btnTestCommand = new Button();
            this.txtTestCommand = new TextBox();
            this.txtLastCommand = new TextBox();
            this.txtResponse = new TextBox();
            this.listViewDevices = new ListView();
            this.lblStatus = new Label();
            this.lblDeviceCount = new Label();
            this.lblTitle = new Label();
            this.lblLastCommand = new Label();
            this.lblResponse = new Label();
            this.lblTestCommand = new Label();
            this.groupBoxVoiceControl = new GroupBox();
            this.groupBoxDevices = new GroupBox();
            this.groupBoxManualTest = new GroupBox();
            this.groupBoxVoiceControl.SuspendLayout();
            this.groupBoxDevices.SuspendLayout();
            this.groupBoxManualTest.SuspendLayout();
            this.SuspendLayout();

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 700);
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Text = "Smart Home Voice Control System";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 700);

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(25, 118, 210);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Size = new Size(400, 41);
            this.lblTitle.Text = "🏠 Smart Home Voice Control";

            // 
            // groupBoxVoiceControl
            // 
            this.groupBoxVoiceControl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.groupBoxVoiceControl.ForeColor = Color.FromArgb(25, 118, 210);
            this.groupBoxVoiceControl.Location = new Point(20, 80);
            this.groupBoxVoiceControl.Size = new Size(460, 280);
            this.groupBoxVoiceControl.Text = "🎤 Voice Control";
            this.groupBoxVoiceControl.Controls.Add(this.btnStartListening);
            this.groupBoxVoiceControl.Controls.Add(this.lblLastCommand);
            this.groupBoxVoiceControl.Controls.Add(this.txtLastCommand);
            this.groupBoxVoiceControl.Controls.Add(this.lblResponse);
            this.groupBoxVoiceControl.Controls.Add(this.txtResponse);
            this.groupBoxVoiceControl.Controls.Add(this.lblStatus);

            // 
            // btnStartListening
            // 
            this.btnStartListening.BackColor = Color.Green;
            this.btnStartListening.ForeColor = Color.White;
            this.btnStartListening.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnStartListening.Location = new Point(20, 40);
            this.btnStartListening.Size = new Size(200, 50);
            this.btnStartListening.Text = "🎤 Start Listening";
            this.btnStartListening.UseVisualStyleBackColor = false;
            this.btnStartListening.Click += new EventHandler(this.btnStartListening_Click);

            // 
            // lblLastCommand
            // 
            this.lblLastCommand.AutoSize = true;
            this.lblLastCommand.Font = new Font("Segoe UI", 9F);
            this.lblLastCommand.ForeColor = Color.Black;
            this.lblLastCommand.Location = new Point(20, 110);
            this.lblLastCommand.Text = "Last Command:";

            // 
            // txtLastCommand
            // 
            this.txtLastCommand.Font = new Font("Segoe UI", 10F);
            this.txtLastCommand.Location = new Point(20, 135);
            this.txtLastCommand.Size = new Size(420, 30);
            this.txtLastCommand.ReadOnly = true;
            this.txtLastCommand.BackColor = Color.LightYellow;

            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Font = new Font("Segoe UI", 9F);
            this.lblResponse.ForeColor = Color.Black;
            this.lblResponse.Location = new Point(20, 180);
            this.lblResponse.Text = "System Response:";

            // 
            // txtResponse
            // 
            this.txtResponse.Font = new Font("Segoe UI", 10F);
            this.txtResponse.Location = new Point(20, 205);
            this.txtResponse.Size = new Size(420, 30);
            this.txtResponse.ReadOnly = true;
            this.txtResponse.BackColor = Color.LightGreen;

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Segoe UI", 9F);
            this.lblStatus.Location = new Point(20, 250);
            this.lblStatus.Size = new Size(420, 20);
            this.lblStatus.Text = "Ready to initialize speech recognition...";

            // 
            // groupBoxManualTest
            // 
            this.groupBoxManualTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.groupBoxManualTest.ForeColor = Color.FromArgb(25, 118, 210);
            this.groupBoxManualTest.Location = new Point(20, 380);
            this.groupBoxManualTest.Size = new Size(460, 120);
            this.groupBoxManualTest.Text = "⌨️ Manual Test";
            this.groupBoxManualTest.Controls.Add(this.lblTestCommand);
            this.groupBoxManualTest.Controls.Add(this.txtTestCommand);
            this.groupBoxManualTest.Controls.Add(this.btnTestCommand);

            // 
            // lblTestCommand
            // 
            this.lblTestCommand.AutoSize = true;
            this.lblTestCommand.Font = new Font("Segoe UI", 9F);
            this.lblTestCommand.ForeColor = Color.Black;
            this.lblTestCommand.Location = new Point(20, 30);
            this.lblTestCommand.Text = "Type command to test:";

            // 
            // txtTestCommand
            // 
            this.txtTestCommand.Font = new Font("Segoe UI", 10F);
            this.txtTestCommand.Location = new Point(20, 55);
            this.txtTestCommand.Size = new Size(300, 30);
            this.txtTestCommand.PlaceholderText = "e.g., turn on living room light";

            // 
            // btnTestCommand
            // 
            this.btnTestCommand.BackColor = Color.FromArgb(25, 118, 210);
            this.btnTestCommand.ForeColor = Color.White;
            this.btnTestCommand.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnTestCommand.Location = new Point(340, 55);
            this.btnTestCommand.Size = new Size(100, 30);
            this.btnTestCommand.Text = "Test";
            this.btnTestCommand.UseVisualStyleBackColor = false;
            this.btnTestCommand.Click += new EventHandler(this.btnTestCommand_Click);

            // 
            // groupBoxDevices
            // 
            this.groupBoxDevices.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.groupBoxDevices.ForeColor = Color.FromArgb(25, 118, 210);
            this.groupBoxDevices.Location = new Point(500, 80);
            this.groupBoxDevices.Size = new Size(480, 420);
            this.groupBoxDevices.Text = "🏠 Connected Devices";
            this.groupBoxDevices.Controls.Add(this.listViewDevices);
            this.groupBoxDevices.Controls.Add(this.lblDeviceCount);

            // 
            // listViewDevices
            // 
            this.listViewDevices.Font = new Font("Segoe UI", 9F);
            this.listViewDevices.FullRowSelect = true;
            this.listViewDevices.GridLines = true;
            this.listViewDevices.Location = new Point(20, 40);
            this.listViewDevices.Size = new Size(440, 340);
            this.listViewDevices.View = View.Details;
            this.listViewDevices.Columns.Add("Device", 180);
            this.listViewDevices.Columns.Add("Type", 120);
            this.listViewDevices.Columns.Add("Status", 120);
            this.listViewDevices.DoubleClick += new EventHandler(this.listViewDevices_DoubleClick);

            // 
            // lblDeviceCount
            // 
            this.lblDeviceCount.AutoSize = true;
            this.lblDeviceCount.Font = new Font("Segoe UI", 9F);
            this.lblDeviceCount.ForeColor = Color.Black;
            this.lblDeviceCount.Location = new Point(20, 390);
            this.lblDeviceCount.Text = "0 of 0 devices active";

            // Add all controls to form
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.groupBoxVoiceControl);
            this.Controls.Add(this.groupBoxManualTest);
            this.Controls.Add(this.groupBoxDevices);

            this.groupBoxVoiceControl.ResumeLayout(false);
            this.groupBoxVoiceControl.PerformLayout();
            this.groupBoxDevices.ResumeLayout(false);
            this.groupBoxDevices.PerformLayout();
            this.groupBoxManualTest.ResumeLayout(false);
            this.groupBoxManualTest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
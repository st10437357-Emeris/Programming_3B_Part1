using System;
using System.Drawing;
using System.IO;
using System.Net.Http.Json;
using System.Windows.Forms;
using SmartX.Core;

namespace SmartX.Dashboard
{
    public partial class IngestionForm : Form
    {
        private string attachedFilePath = string.Empty;
        private System.Windows.Forms.Timer telemetryTimer;
        private Panel visualIndicator = new Panel();
        private Random rand = new Random();

        public IngestionForm()
        {
            InitializeComponent();
            BuildUI();
            SetupDynamicEngagementFeature();
        }

        private void BuildUI()
        {
            this.Text = "Sensor Data Ingestion & Configuration";
            this.Size = new Size(500, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Device MAC Address
            this.Controls.Add(new Label { Text = "Device MAC Address:", Location = new Point(30, 30), AutoSize = true });
            TextBox txtMac = new TextBox { Name = "txtMac", Location = new Point(200, 30), Width = 230 };
            this.Controls.Add(txtMac);

            // Sensor Deployment Location
            this.Controls.Add(new Label { Text = "Deployment Location:", Location = new Point(30, 80), AutoSize = true });
            TextBox txtLocation = new TextBox { Name = "txtLocation", Location = new Point(200, 80), Width = 230 };
            this.Controls.Add(txtLocation);

            // Sensor Category Selection
            this.Controls.Add(new Label { Text = "Sensor Category:", Location = new Point(30, 130), AutoSize = true });
            ComboBox cmbCategory = new ComboBox { Name = "cmbCategory", Location = new Point(200, 130), Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCategory.Items.AddRange(new string[] { "Environmental (Float)", "Power Consumption (Int)", "Actuator (Bool)" });
            cmbCategory.SelectedIndex = 0;
            this.Controls.Add(cmbCategory);

            // Media/Log Attachment
            Label lblFileStatus = new Label { Name = "lblFileStatus", Text = "No file attached.", Location = new Point(200, 215), AutoSize = true, ForeColor = Color.Gray };
            this.Controls.Add(lblFileStatus);

            Button btnAttach = new Button { Text = "Attach Config/Log File", Location = new Point(200, 180), Width = 230 };
            btnAttach.Click += (s, e) =>
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        attachedFilePath = ofd.FileName;
                        lblFileStatus.Text = $"Attached: {Path.GetFileName(attachedFilePath)}";
                        lblFileStatus.ForeColor = Color.Green;
                    }
                }
            };
            this.Controls.Add(btnAttach);

            // Dynamic Engagement UI: Visual Telemetry Feedback
            this.Controls.Add(new Label { Text = "Live Node Health:", Location = new Point(30, 320), AutoSize = true, Font = new Font("Arial", 10, FontStyle.Bold) });
            visualIndicator.Location = new Point(200, 315);
            visualIndicator.Size = new Size(230, 30);
            visualIndicator.BackColor = Color.LightGray;
            this.Controls.Add(visualIndicator);

            Button btnSimulate = new Button { Text = "Start Telemetry Stream", Location = new Point(200, 360), Width = 230 };
            btnSimulate.Click += (s, e) => telemetryTimer.Start();
            this.Controls.Add(btnSimulate);

            // Submit Registration (API Push)
            Button btnSubmit = new Button { Text = "Register Sensor", Location = new Point(200, 420), Width = 230, Height = 40, BackColor = Color.LightGreen };
            btnSubmit.Click += async (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtMac.Text) || string.IsNullOrWhiteSpace(txtLocation.Text))
                {
                    MessageBox.Show("Please fill in all sensor registration fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var packet = new TelemetryPacket<string>
                {
                    MacAddress = txtMac.Text,
                    Location = txtLocation.Text,
                    Payload = cmbCategory.SelectedItem?.ToString() ?? "Unknown"
                };

                try
                {
                    var response = await Form1.ApiClient.PostAsJsonAsync("api/telemetry/ingest", packet);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Sensor {txtMac.Text} successfully ingested by the Smart-X API!", "API Success");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("API rejected the payload.", "API Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to connect to API. Ensure SmartX_Ecosystem is running.\n{ex.Message}", "Connection Error");
                }
            };
            this.Controls.Add(btnSubmit);
        }

        private void SetupDynamicEngagementFeature()
        {
            telemetryTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            telemetryTimer.Tick += (s, e) =>
            {
                int simulatedPayload = rand.Next(40, 120);
                if (simulatedPayload > 95) visualIndicator.BackColor = Color.Red;
                else if (simulatedPayload > 75) visualIndicator.BackColor = Color.Orange;
                else visualIndicator.BackColor = Color.LimeGreen;
            };
        }
    }
}
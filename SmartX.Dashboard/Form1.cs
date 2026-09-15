using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

namespace SmartX.Dashboard
{
    public partial class Form1 : Form
    {
        // HTTP Client to satisfy the API Integration Layer requirement
        public static readonly HttpClient ApiClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7245/") // Use YOUR specific port here
        };

        public Form1()
        {
            InitializeComponent();
            BuildStartupInterface();
        }

        private void BuildStartupInterface()
        {
            this.Text = "Smart-X Application Gateway";
            this.Size = new Size(500, 350);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Pillar 1: Sensor Data Ingestion and Telemetry (Active)
            Button btnIngestion = new Button
            {
                Text = "Sensor Data Ingestion and Telemetry",
                Size = new Size(350, 50),
                Location = new Point(65, 40),
                BackColor = Color.LightSkyBlue
            };

            // UPDATED: Now opens the new Ingestion module where payload management occurs
            btnIngestion.Click += (s, e) =>
            {
                IngestionForm ingestionForm = new IngestionForm();
                ingestionForm.Show();
            };

            this.Controls.Add(btnIngestion);

            // Pillar 2: Real-Time Command Stream and History (Disabled)
            Button btnCommand = new Button
            {
                Text = "Real-Time Command Stream and History\n(To be implemented in Part 2)",
                Size = new Size(350, 50),
                Location = new Point(65, 110),
                Enabled = false // Disabled as per requirements
            };
            this.Controls.Add(btnCommand);

            // Pillar 3: Network Topology and Mesh Routing (Disabled)
            Button btnTopology = new Button
            {
                Text = "Network Topology and Mesh Routing\n(To be implemented in final PoE)",
                Size = new Size(350, 50),
                Location = new Point(65, 180),
                Enabled = false // Disabled as per requirements
            };
            this.Controls.Add(btnTopology);
        }
    }
}
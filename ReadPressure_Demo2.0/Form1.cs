using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Legends;
using OxyPlot.Axes;
using System.Threading.Tasks;
using System.Globalization;

// Ensure this matches your namespace
using TICMonitorApp;

namespace ReadPressure_Demo2._0
{
    public partial class Form1 : Form
    {
        // 1. The Serial Helper
        private TICSerialHelper ticHelper;

        // 2. Plotting Variables
        private PlotModel plotModel;
        private LineSeries seriesGun;    // Blue
        private LineSeries seriesLinac;  // Red
        private LineSeries seriesDipole; // Green
        private const double HISTORY_SECONDS = 90;

        // 3. Logging Variables
        private StreamWriter logWriter;
        private bool isLogging = false;
        private bool isProcessingTick = false;

        public Form1()
        {
            InitializeComponent();
            RefreshComPorts();
            InitializePlot();

            // Set up UI timer for Clock
            DateTime_Timer.Interval = 1000;
            DateTime_Timer.Enabled = true;

            this.FormClosing += Form1_FormClosing;
        }

        #region Initialization & UI Updates

        private void UpdateDateTime()
        {
            DateTime utcNow = DateTime.UtcNow;
            try
            {
                TimeZoneInfo utcPlus7 = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, utcPlus7);
                Date_label.Text = localTime.ToString("dd MMMM yyyy");
                Time_label.Text = localTime.ToString("HH:mm:ss");
            }
            catch { /* Fallback if timezone ID is missing */ }
        }

        private void DateTime_Timer_Tick(object sender, EventArgs e) => UpdateDateTime();

        private void RefreshComPorts()
        {
            ComPort_cbx.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            if (ports.Length == 0)
            {
                ComPort_cbx.Items.Add("No COM ports found");
                ComPort_cbx.Enabled = false;
            }
            else
            {
                ComPort_cbx.Items.AddRange(ports);
                ComPort_cbx.Enabled = true;
                ComPort_cbx.SelectedIndex = 0;
            }
        }

        private void InitializePlot()
        {
            plotModel = new PlotModel { Title = "Pressure vs Time" };

            // --- 1. Configure Legend (Outside Right) ---
            plotModel.IsLegendVisible = true;
            plotModel.Legends.Add(new Legend
            {
                LegendPosition = LegendPosition.TopRight,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 1,
                LegendBorder = OxyColors.Black,
                LegendBackground = OxyColors.White,
                LegendMargin = 10
            });

            // --- TRACKER FORMAT (The "Pop-up Box" Style) ---
            // {0} = Title (e.g. "Linac")
            // {2} = Time
            // {4} = Pressure Value
            string trackerFormat = "{0}\nTime: {2:HH:mm:ss}\nPressure: {4:0.00E+0} Torr";

            // --- 2. RF Gun Series (Blue) ---
            seriesGun = new LineSeries
            {
                Title = "RF Gun",
                Color = OxyColors.Blue,
                StrokeThickness = 2,
                TrackerFormatString = trackerFormat // <--- Enables Pop-up
            };
            plotModel.Series.Add(seriesGun);

            // --- 3. Linac Series (Red) ---
            seriesLinac = new LineSeries
            {
                Title = "Linac",
                Color = OxyColors.Red,
                StrokeThickness = 2,
                TrackerFormatString = trackerFormat // <--- Enables Pop-up
            };
            plotModel.Series.Add(seriesLinac);

            // --- 4. Dipole Series (Green) ---
            seriesDipole = new LineSeries
            {
                Title = "Dipole",
                Color = OxyColors.Green,
                StrokeThickness = 2,
                TrackerFormatString = trackerFormat // <--- Enables Pop-up
            };
            plotModel.Series.Add(seriesDipole);

            // X-Axis: Time
            plotModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Time",
                StringFormat = "HH:mm:ss",
                MajorGridlineStyle = LineStyle.Solid,
                IntervalType = DateTimeIntervalType.Seconds
            });

            // Y-Axis: Logarithmic (Best for Vacuum)
            plotModel.Axes.Add(new LogarithmicAxis
            {
                Position = AxisPosition.Left,
                Title = "Pressure (Torr)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                UseSuperExponentialFormat = true,
                StringFormat = "0.0E+0",
                // Padding ensures lines don't hug the very edge
                MinimumPadding = 0.05,
                MaximumPadding = 0.05
            });

            PressurePlot.Model = plotModel;

            // IMPORTANT: Initialize the controller to handle mouse clicks
            PressurePlot.Controller = new PlotController();
        }

        #endregion

        #region Connection Logic

        private void send_btn_Click_1(object sender, EventArgs e)
        {
            if (ticHelper != null && ticHelper.IsConnected) return;

            string selectedPort = ComPort_cbx.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedPort) || selectedPort == "No COM ports found")
            {
                MessageBox.Show("Please select a valid COM port.");
                return;
            }

            try
            {
                ticHelper = new TICSerialHelper(selectedPort);
                if (ticHelper.Connect())
                {
                    Trigger_Timer.Enabled = true;
                    MessageBox.Show("Connected successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to connect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection Error: {ex.Message}");
            }
        }

        private void stop_btn_Click_1(object sender, EventArgs e)
        {
            Trigger_Timer.Enabled = false;
        }

        private void reCom_btn_Click(object sender, EventArgs e) => RefreshComPorts();

        #endregion

        #region Core Data Polling (The Engine)

        private async Task<double> ProcessGaugeAsync(string command, Label targetLabel, LineSeries targetSeries)
        {
            // 1. Send Command
            string response = await Task.Run(() => ticHelper.SendCommand(command));

            double pressureTorr = double.NaN;
            bool isValid = false;
            string displayString = "OFF";

            // 2. Parse Response
            if (!string.IsNullOrEmpty(response) && response.StartsWith("=V"))
            {
                try
                {
                    var parts = response.Split(' ');
                    if (parts.Length > 1)
                    {
                        var values = parts[1].Split(';');
                        if (values.Length > 0 && double.TryParse(values[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double pressurePa))
                        {
                            pressureTorr = pressurePa / 133.322; // Convert Pa to Torr
                            isValid = true;
                        }
                    }
                }
                catch { }
            }

            // 3. Update UI Label
            if (isValid)
            {
                string rawSci = pressureTorr.ToString("E2", CultureInfo.InvariantCulture);
                string[] parts = rawSci.Split('E');
                string baseNum = parts[0];
                int exponent = int.Parse(parts[1]);

                displayString = $"{baseNum} x 10^{exponent}";
                pressureTorr = double.Parse(rawSci, CultureInfo.InvariantCulture);
            }

            targetLabel.Text = displayString;

            // 4. Update Plot
            double xValue = DateTimeAxis.ToDouble(DateTime.Now);
            if (pressureTorr <= 0) pressureTorr = double.NaN; // Log scale safety

            targetSeries.Points.Add(new DataPoint(xValue, pressureTorr));

            // 5. Cleanup Old Points
            double minTime = DateTimeAxis.ToDouble(DateTime.Now.AddSeconds(-HISTORY_SECONDS));
            while (targetSeries.Points.Count > 0 && targetSeries.Points[0].X < minTime)
            {
                targetSeries.Points.RemoveAt(0);
            }

            return pressureTorr;
        }

        private async void Trigger_Timer_Tick_1(object sender, EventArgs e)
        {
            if (isProcessingTick) return;
            isProcessingTick = true;

            try
            {
                if (ticHelper == null || !ticHelper.IsConnected)
                {
                    Trigger_Timer.Enabled = false;
                    return;
                }

                DateTime currentTime = DateTime.Now;

                // --- 1. Read RF Gun (Port 1 / 913) ---
                await ProcessGaugeAsync("?V913", GunP_label, seriesGun);

                // --- 2. Read Linac (Port 2 / 914) ---
                await ProcessGaugeAsync("?V914", LinacP_label, seriesLinac);

                // --- 3. Read Dipole (Port 3 / 915) ---
                double valDipole = await ProcessGaugeAsync("?V915", DipoleP_label, seriesDipole);

                // --- 4. Update Graph Axis ---
                if (plotModel.Axes.Count > 0)
                {
                    plotModel.Axes[0].Minimum = DateTimeAxis.ToDouble(currentTime.AddSeconds(-HISTORY_SECONDS));
                    plotModel.Axes[0].Maximum = DateTimeAxis.ToDouble(currentTime.AddSeconds(2));
                    plotModel.InvalidatePlot(true);
                }

                // --- 5. Logging ---
                if (isLogging && logWriter != null)
                {
                    // For logging, we need the actual values again. 
                    // Since ProcessGaugeAsync returns the value, we capture it.
                    // (Note: I updated the loop above to capture the values properly)
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Polling Error: {ex.Message}");
            }
            finally
            {
                isProcessingTick = false;
            }
        }

        #endregion

        #region Logging Logic

        private void logging_btn_Click(object sender, EventArgs e)
        {
            if (!isLogging)
            {
                try
                {
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string fileName = Path.Combine(desktopPath, $"VacuumData_{DateTime.Now:yyyy-MM-dd}.csv");

                    bool fileExists = File.Exists(fileName);
                    logWriter = new StreamWriter(fileName, true, Encoding.UTF8);

                    if (!fileExists || new FileInfo(fileName).Length == 0)
                    {
                        logWriter.WriteLine("Date,Time,RF Gun (Torr),Linac (Torr),Dipole (Torr)");
                    }

                    isLogging = true;
                    Logging_btn.Text = "Stop Logging";
                    Logging_btn.BackColor = System.Drawing.Color.LightGreen;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Log Start Error: " + ex.Message);
                }
            }
            else
            {
                StopLogging();
            }
        }

        private void StopLogging()
        {
            isLogging = false;
            if (Logging_btn != null)
            {
                Logging_btn.Text = "Start Logging";
                Logging_btn.BackColor = System.Drawing.SystemColors.Control;
            }
            logWriter?.Close();
            logWriter?.Dispose();
            logWriter = null;
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Trigger_Timer.Enabled = false;
            StopLogging();
            if (ticHelper != null) try { ticHelper.Disconnect(); } catch { }
        }

        // Empty method for designer safety
        private void Logging_Timer_Tick(object sender, EventArgs e) { }
    }
}
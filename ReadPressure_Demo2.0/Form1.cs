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
using System.Drawing; // Required for changing Button Colors

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

        // --- SETTINGS ---
        private const double HISTORY_SECONDS = 90; // Width of the "Live" window
        private const int MAX_POINTS = 90000;      // 5 Hours Buffer (FIFO)
        private DateTime lastPlotTime = DateTime.MinValue;

        // 3. UI State Variables
        private bool isAutoScroll = true;

        // 4. Logging Variables
        private StreamWriter logWriter;
        private bool isLogging = false;
        private bool isProcessingTick = false;

        public Form1()
        {
            InitializeComponent();

            // Link the View Button manually (safety check)
            this.View_btn.Click += new EventHandler(this.View_btn_Click);

            RefreshComPorts();
            InitializePlot();
            UpdateViewButtonState();

            // UI Clock
            DateTime_Timer.Interval = 1000;
            DateTime_Timer.Enabled = true;

            // Default Polling Speed (Idle = 2Hz)
            Trigger_Timer.Interval = 500;

            this.FormClosing += Form1_FormClosing;
        }

        #region Initialization & Plot Setup

        private void InitializePlot()
        {
            plotModel = new PlotModel { Title = "Pressure vs Time" };

            // Legend
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

            // Series Setup
            string trackerFormat = "{0}\nTime: {2:HH:mm:ss}\nPressure: {4:0.00E+0} Torr";
            seriesGun = new LineSeries { Title = "RF Gun", Color = OxyColors.Blue, StrokeThickness = 2, TrackerFormatString = trackerFormat };
            seriesLinac = new LineSeries { Title = "Linac", Color = OxyColors.Red, StrokeThickness = 2, TrackerFormatString = trackerFormat };
            seriesDipole = new LineSeries { Title = "Dipole", Color = OxyColors.Green, StrokeThickness = 2, TrackerFormatString = trackerFormat };

            plotModel.Series.Add(seriesGun);
            plotModel.Series.Add(seriesLinac);
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

            // Y-Axis: Logarithmic (With Safety Padding)
            plotModel.Axes.Add(new LogarithmicAxis
            {
                Position = AxisPosition.Left,
                Title = "Pressure (Torr)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                UseSuperExponentialFormat = true,
                StringFormat = "0.0E+0",

                // PADDING prevents line from sticking to edges
                MinimumPadding = 0.1,
                MaximumPadding = 0.1,

                // DEFAULT VIEW: Helps find the line immediately
                Minimum = 1e-10,
                Maximum = 1e-5
            });

            PressurePlot.Model = plotModel;
            PressurePlot.Controller = new PlotController();

            // --- MOUSE INTERACTIONS ---
            // Right-Click -> History Mode
            PressurePlot.MouseDown += (s, e) => {
                if (e.Button == MouseButtons.Right)
                {
                    this.Invoke((MethodInvoker)delegate {
                        isAutoScroll = false;
                        UpdateViewButtonState();
                    });
                }
            };

            // Double-Click -> Live Mode & Reset Zoom
            PressurePlot.MouseDoubleClick += (s, e) => {
                this.Invoke((MethodInvoker)delegate {
                    isAutoScroll = true;
                    UpdateViewButtonState();
                    plotModel.ResetAllAxes(); // Auto-Find the line!
                    plotModel.InvalidatePlot(false);
                });
            };
        }

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

        private void UpdateDateTime()
        {
            try
            {
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                Date_label.Text = localTime.ToString("dd MMMM yyyy");
                Time_label.Text = localTime.ToString("HH:mm:ss");
            }
            catch { }
        }

        private void DateTime_Timer_Tick(object sender, EventArgs e) => UpdateDateTime();
        private void reCom_btn_Click(object sender, EventArgs e) => RefreshComPorts();

        #endregion

        #region Connection Logic

        private void send_btn_Click(object sender, EventArgs e)
        {
            // 1. If already connected, do nothing
            if (ticHelper != null && ticHelper.IsConnected) return;

            string selectedPort = ComPort_cbx.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedPort) || selectedPort == "No COM ports found")
            {
                MessageBox.Show("Please select a valid COM port.");
                return;
            }

            try
            {
                // 2. Connect to Hardware
                ticHelper = new TICSerialHelper(selectedPort);
                if (ticHelper.Connect())
                {
                    // --- FIX: INSERT INVISIBLE POINTS TO CREATE A GAP ---
                    // This tells OxyPlot to lift the pen and not connect the old data to the new data.
                    double now = DateTimeAxis.ToDouble(DateTime.Now);

                    // Add NaN (Not a Number) to break the line visual
                    seriesGun.Points.Add(new DataPoint(now, double.NaN));
                    seriesLinac.Points.Add(new DataPoint(now, double.NaN));
                    seriesDipole.Points.Add(new DataPoint(now, double.NaN));
                    // ---------------------------------------------------

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

        private void stop_btn_Click(object sender, EventArgs e)
        {
            // 1. STOP TIMER (Prevent new data requests)
            Trigger_Timer.Enabled = false;

            // 2. SAFE LOGGING SHUTDOWN (Auto-save if user forgot)
            if (isLogging)
            {
                StopLogging(); // This closes the file stream safely
                MessageBox.Show("Logging was active and has been saved automatically.");
            }

            // 3. DISCONNECT HARDWARE
            if (ticHelper != null && ticHelper.IsConnected)
            {
                ticHelper.Disconnect();
                MessageBox.Show("Monitoring Stopped & Disconnected.");
            }
            else
            {
                // Optional: If already disconnected, just visually confirm stop
                // MessageBox.Show("Monitoring Stopped.");
            }
        }

        #endregion

        #region Core Data Polling & Plotting

        private async Task<double> ProcessGaugeAsync(string command, Label targetLabel)
        {
            string response = await Task.Run(() => ticHelper.SendCommand(command));

            double pressureTorr = double.NaN;
            bool isValid = false;
            string displayString = "OFF";

            if (!string.IsNullOrEmpty(response) && response.StartsWith("=V"))
            {
                try
                {
                    var parts = response.Split(' ');
                    if (parts.Length > 1)
                    {
                        var values = parts[1].Split(';');
                        // Parse Pressure (Pa -> Torr)
                        if (values.Length > 0 && double.TryParse(values[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double pressurePa))
                        {
                            pressureTorr = pressurePa / 133.322;
                            isValid = true;
                        }
                    }
                }
                catch { }
            }

            // Update Label
            if (isValid)
            {
                string rawSci = pressureTorr.ToString("E2", CultureInfo.InvariantCulture);
                string[] parts = rawSci.Split('E');
                displayString = $"{parts[0]} x 10^{parts[1]}";
                pressureTorr = double.Parse(rawSci, CultureInfo.InvariantCulture);
            }

            targetLabel.Text = displayString;
            return pressureTorr;
        }

        private async void Trigger_Timer_Tick(object sender, EventArgs e)
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

                // 1. ACQUIRE DATA (REAL HARDWARE)
                double valGun = await ProcessGaugeAsync("?V913", GunP_label);
                double valLinac = await ProcessGaugeAsync("?V914", LinacP_label);
                double valDipole = await ProcessGaugeAsync("?V915", DipoleP_label);

                DateTime now = DateTime.Now;

                // 2. LOG DATA (High Res: 5Hz when active)
                if (isLogging && logWriter != null)
                {
                    await logWriter.WriteLineAsync($"{now:yyyy-MM-dd},{now:HH:mm:ss.fff},{valGun:E4},{valLinac:E4},{valDipole:E4}");
                }

                // 3. PLOT DATA (Throttled: 2Hz)
                if ((now - lastPlotTime).TotalMilliseconds >= 500)
                {
                    double xValue = DateTimeAxis.ToDouble(now);

                    AddPointToSeries(seriesGun, xValue, valGun);
                    AddPointToSeries(seriesLinac, xValue, valLinac);
                    AddPointToSeries(seriesDipole, xValue, valDipole);

                    if (isAutoScroll && plotModel.Axes.Count > 0)
                    {
                        var xAxis = plotModel.Axes[0];
                        xAxis.Minimum = DateTimeAxis.ToDouble(now.AddSeconds(-HISTORY_SECONDS));
                        xAxis.Maximum = DateTimeAxis.ToDouble(now.AddSeconds(2));
                    }

                    plotModel.InvalidatePlot(false);
                    lastPlotTime = now;
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

        private void AddPointToSeries(LineSeries series, double x, double y)
        {
            if (y <= 0 || double.IsNaN(y)) y = double.NaN;
            series.Points.Add(new DataPoint(x, y));

            if (series.Points.Count > MAX_POINTS)
            {
                series.Points.RemoveAt(0);
            }
        }

        #endregion

        #region User Controls

        private void View_btn_Click(object sender, EventArgs e)
        {
            isAutoScroll = !isAutoScroll;
            UpdateViewButtonState();
        }

        private void UpdateViewButtonState()
        {
            if (isAutoScroll)
            {
                View_btn.Text = "Live";
                View_btn.BackColor = Color.LightGreen;
            }
            else
            {
                View_btn.Text = "History";
                View_btn.BackColor = Color.Gold;
            }
        }

        private void logging_btn_Click(object sender, EventArgs e)
        {
            if (!isLogging)
            {
                // --- START LOGGING ---
                try
                {
                    // 1. Define Paths
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string folderPath = Path.Combine(desktopPath, "VacuumLogs");

                    // 2. CHECK FOLDER: Create it if it doesn't exist
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // 3. CHECK FILE: Use TODAY'S DATE for the name (No hours/minutes)
                    // This ensures we always find the same file for the whole day.
                    string fileName = Path.Combine(folderPath, $"VacuumData_{DateTime.Now:yyyy-MM-dd}.csv");

                    // 4. Check if this is a fresh start (File missing or empty)
                    bool isNewFile = !File.Exists(fileName) || new FileInfo(fileName).Length == 0;

                    // 5. OPEN FILE: The 'true' parameter means APPEND mode
                    logWriter = new StreamWriter(fileName, true, Encoding.UTF8);
                    logWriter.AutoFlush = true; // Force save immediately

                    // 6. HEADER LOGIC:
                    if (isNewFile)
                    {
                        // Only write the header if the file is brand new
                        logWriter.WriteLine("Date,Time,RF Gun (Torr),Linac (Torr),Dipole (Torr)");
                    }
                    else
                    {
                        // If appending, add a separator so you know where you resumed
                        logWriter.WriteLine("--- Resumed Logging ---");
                    }

                    isLogging = true;

                    // Speed up polling to 5Hz
                    Trigger_Timer.Interval = 200;

                    Logging_btn.Text = "Stop Logging";
                    Logging_btn.BackColor = Color.LightGreen;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Log Start Error: " + ex.Message);
                }
            }
            else
            {
                // --- STOP LOGGING ---
                StopLogging();

                // Slow down polling to 2Hz
                Trigger_Timer.Interval = 500;

                Logging_btn.Text = "Start Logging";
                Logging_btn.BackColor = SystemColors.Control;
            }
        }

        private void StopLogging()
        {
            isLogging = false;
            try
            {
                if (logWriter != null)
                {
                    logWriter.Close();
                    logWriter.Dispose();
                    logWriter = null;
                }
            }
            catch { }
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Trigger_Timer.Enabled = false;
            StopLogging();
            if (ticHelper != null) try { ticHelper.Disconnect(); } catch { }
        }
    }
}
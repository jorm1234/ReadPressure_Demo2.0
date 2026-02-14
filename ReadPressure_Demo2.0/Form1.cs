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
using OxyPlot.Annotations;
using System.Threading.Tasks;
using System.Globalization;
using System.Drawing;

// Ensure this matches your namespace
using TICMonitorApp;

namespace ReadPressure_Demo2._0
{
    public partial class Form1 : Form
    {
        // 1. Serial & Plot Variables
        private TICSerialHelper ticHelper;
        private PlotModel plotModel;
        private LineSeries seriesGun, seriesLinac, seriesDipole;

        // --- ALARM VARIABLES ---
        // These will be loaded from your TextBoxes instantly
        private double limitGun;
        private double limitLinac;
        private double limitDipole;

        // Blinking State (True/False toggle)
        private bool isAlarmBlinkOn = false;

        // Visual Plot Lines
        private LineAnnotation lineGun, lineLinac, lineDipole;
        // -----------------------

        // Settings
        private const double HISTORY_SECONDS = 90;
        private const int MAX_POINTS = 90000;
        private DateTime lastPlotTime = DateTime.MinValue;

        // State
        private bool isAutoScroll = true;
        private StreamWriter logWriter;
        private bool isLogging = false;
        private bool isProcessingTick = false;

        public Form1()
        {
            InitializeComponent();

            // Link Buttons manually for safety
            this.View_btn.Click += new EventHandler(this.View_btn_Click);
            this.SetLim_btn.Click += new EventHandler(this.SetLim_btn_Click);

            RefreshComPorts();
            InitializePlot();
            UpdateViewButtonState();

            // --- CRITICAL: READ YOUR TEXTBOX DEFAULTS (1e-8, 1e-6, etc.) ---
            UpdateLimitsFromTextBoxes();
            // ---------------------------------------------------------------

            // UI Clock
            DateTime_Timer.Interval = 1000;
            DateTime_Timer.Enabled = true;

            // Default Speed
            Trigger_Timer.Interval = 500;

            this.FormClosing += Form1_FormClosing;
        }

        // --- HELPER: Read TextBoxes & Update Variables ---
        private void UpdateLimitsFromTextBoxes()
        {
            try
            {
                // Parse the values from TextBoxes
                limitGun = double.Parse(GunLim_txb.Text, NumberStyles.Any, CultureInfo.InvariantCulture);
                limitLinac = double.Parse(LinLim_txb.Text, NumberStyles.Any, CultureInfo.InvariantCulture);
                limitDipole = double.Parse(DipLim_txb.Text, NumberStyles.Any, CultureInfo.InvariantCulture);

                // --- THE FIX: Update Y position, and FORCE TEXT TO NULL ---
                if (lineGun != null)
                {
                    lineGun.Y = limitGun;
                    lineGun.Text = null; // Forces label to disappear
                }

                if (lineLinac != null)
                {
                    lineLinac.Y = limitLinac;
                    lineLinac.Text = null; // Forces label to disappear
                }

                if (lineDipole != null)
                {
                    lineDipole.Y = limitDipole;
                    lineDipole.Text = null; // Forces label to disappear
                }

                if (plotModel != null) plotModel.InvalidatePlot(false);
            }
            catch
            {
                MessageBox.Show("Error reading limits! Check number format.");
            }
        }

        #region Initialization & Plot

        private void InitializePlot()
        {
            plotModel = new PlotModel { Title = "Pressure vs Time" };

            // --- LEGEND ---
            plotModel.IsLegendVisible = true;
            plotModel.Legends.Add(new Legend
            {
                LegendPosition = LegendPosition.BottomCenter,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Horizontal
            });

            // --- DATA SERIES (Solid Lines) ---
            string trackerFormat = "{0}\nTime: {2:HH:mm:ss}\nPressure: {4:0.00E+0} Torr";

            seriesGun = new LineSeries { Title = "RF Gun", Color = OxyColors.Blue, StrokeThickness = 2, TrackerFormatString = trackerFormat };
            seriesLinac = new LineSeries { Title = "Linac", Color = OxyColors.Red, StrokeThickness = 2, TrackerFormatString = trackerFormat };
            seriesDipole = new LineSeries { Title = "Dipole", Color = OxyColors.Green, StrokeThickness = 2, TrackerFormatString = trackerFormat };

            plotModel.Series.Add(seriesGun);
            plotModel.Series.Add(seriesLinac);
            plotModel.Series.Add(seriesDipole);

            // --- LEGEND ENTRIES FOR LIMITS (Dummy Series) ---
            // These ensure "Dashed Line" appears in the Legend Box only
            var legGun = new LineSeries { Title = "Gun Limit", Color = OxyColors.Blue, LineStyle = LineStyle.Dash, StrokeThickness = 2 };
            legGun.Points.Add(new DataPoint(double.NaN, double.NaN));
            plotModel.Series.Add(legGun);

            var legLin = new LineSeries { Title = "Linac Limit", Color = OxyColors.Red, LineStyle = LineStyle.Dash, StrokeThickness = 2 };
            legLin.Points.Add(new DataPoint(double.NaN, double.NaN));
            plotModel.Series.Add(legLin);

            var legDip = new LineSeries { Title = "Dipole Limit", Color = OxyColors.Green, LineStyle = LineStyle.Dash, StrokeThickness = 2 };
            legDip.Points.Add(new DataPoint(double.NaN, double.NaN));
            plotModel.Series.Add(legDip);

            // --- ACTUAL LIMIT LINES (Annotations) ---
            // Text is set to NULL to ensure NO LABEL on the chart
            lineGun = new LineAnnotation { Type = LineAnnotationType.Horizontal, Y = limitGun, Color = OxyColors.Blue, LineStyle = LineStyle.Dash, StrokeThickness = 2, Text = null };
            plotModel.Annotations.Add(lineGun);

            lineLinac = new LineAnnotation { Type = LineAnnotationType.Horizontal, Y = limitLinac, Color = OxyColors.Red, LineStyle = LineStyle.Dash, StrokeThickness = 2, Text = null };
            plotModel.Annotations.Add(lineLinac);

            lineDipole = new LineAnnotation { Type = LineAnnotationType.Horizontal, Y = limitDipole, Color = OxyColors.Green, LineStyle = LineStyle.Dash, StrokeThickness = 2, Text = null };
            plotModel.Annotations.Add(lineDipole);

            // --- AXES ---
            plotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm:ss", Title = "Time" });
            plotModel.Axes.Add(new LogarithmicAxis
            {
                Position = AxisPosition.Left,
                Title = "Pressure (Torr)",
                UseSuperExponentialFormat = true,
                StringFormat = "0.0E+0",
                Minimum = 1e-10,
                Maximum = 1e-5
            });

            PressurePlot.Model = plotModel;
            PressurePlot.Controller = new PlotController();
        }

        private void RefreshComPorts()
        {
            ComPort_cbx.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length == 0) { ComPort_cbx.Items.Add("No COM ports found"); ComPort_cbx.Enabled = false; }
            else { ComPort_cbx.Items.AddRange(ports); ComPort_cbx.Enabled = true; ComPort_cbx.SelectedIndex = 0; }
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

        #region Connection

        private void send_btn_Click(object sender, EventArgs e)
        {
            if (ticHelper != null && ticHelper.IsConnected) return;
            string port = ComPort_cbx.SelectedItem as string;
            if (string.IsNullOrEmpty(port) || port.Contains("No COM")) return;

            try
            {
                ticHelper = new TICSerialHelper(port);
                if (ticHelper.Connect())
                {
                    // Gap Logic
                    double now = DateTimeAxis.ToDouble(DateTime.Now);
                    seriesGun.Points.Add(new DataPoint(now, double.NaN));
                    seriesLinac.Points.Add(new DataPoint(now, double.NaN));
                    seriesDipole.Points.Add(new DataPoint(now, double.NaN));

                    Trigger_Timer.Enabled = true;
                    MessageBox.Show("Connected.");
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            Trigger_Timer.Enabled = false;
            if (isLogging) { StopLogging(); MessageBox.Show("Log Saved."); }
            if (ticHelper != null) { ticHelper.Disconnect(); MessageBox.Show("Disconnected."); }
        }

        #endregion

        #region Main Loop (Tick)

        private async void Trigger_Timer_Tick(object sender, EventArgs e)
        {
            if (isProcessingTick) return;
            isProcessingTick = true;

            try
            {
                if (ticHelper == null || !ticHelper.IsConnected) { Trigger_Timer.Enabled = false; return; }

                // 1. Get Data
                double valGun = await ProcessGaugeAsync("?V913", GunP_label);
                double valLinac = await ProcessGaugeAsync("?V914", LinacP_label);
                double valDipole = await ProcessGaugeAsync("?V915", DipoleP_label);
                DateTime now = DateTime.Now;

                // 2. Log
                if (isLogging && logWriter != null)
                {
                    await logWriter.WriteLineAsync($"{now:yyyy-MM-dd},{now:HH:mm:ss.fff},{valGun:E4},{valLinac:E4},{valDipole:E4}");
                }

                // 3. Plot (2Hz limit)
                if ((now - lastPlotTime).TotalMilliseconds >= 500)
                {
                    double xVal = DateTimeAxis.ToDouble(now);
                    AddPointToSeries(seriesGun, xVal, valGun);
                    AddPointToSeries(seriesLinac, xVal, valLinac);
                    AddPointToSeries(seriesDipole, xVal, valDipole);

                    if (isAutoScroll && plotModel.Axes.Count > 0)
                    {
                        var xAxis = plotModel.Axes[0];
                        xAxis.Minimum = DateTimeAxis.ToDouble(now.AddSeconds(-HISTORY_SECONDS));
                        xAxis.Maximum = DateTimeAxis.ToDouble(now.AddSeconds(2));
                    }
                    plotModel.InvalidatePlot(false);
                    lastPlotTime = now;
                }

                // 4. ALARM BLINKING LOGIC
                isAlarmBlinkOn = !isAlarmBlinkOn; // Toggle Tick

                CheckAlarm(GunP_label, valGun, limitGun);
                CheckAlarm(LinacP_label, valLinac, limitLinac);
                CheckAlarm(DipoleP_label, valDipole, limitDipole);
            }
            catch { }
            finally { isProcessingTick = false; }
        }

        private void CheckAlarm(Label lbl, double val, double limit)
        {
            if (val > limit)
            {
                // BLINKING: Red Background <-> Control Background
                if (isAlarmBlinkOn)
                {
                    lbl.BackColor = Color.Red;
                    lbl.ForeColor = Color.White;
                }
                else
                {
                    lbl.BackColor = SystemColors.Control;
                    lbl.ForeColor = Color.Red; // Keep text red during 'off' phase
                }
            }
            else
            {
                // NORMAL
                lbl.BackColor = SystemColors.Control;
                lbl.ForeColor = Color.Black;
            }
        }

        private async Task<double> ProcessGaugeAsync(string cmd, Label lbl)
        {
            string resp = await Task.Run(() => ticHelper.SendCommand(cmd));
            double pTorr = double.NaN;
            bool valid = false;

            if (!string.IsNullOrEmpty(resp) && resp.StartsWith("=V"))
            {
                try
                {
                    var parts = resp.Split(';')[0].Split(' ');
                    if (parts.Length > 1 && double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double pa))
                    {
                        pTorr = pa / 133.322;
                        valid = true;
                    }
                }
                catch { }
            }

            if (valid)
            {
                string sci = pTorr.ToString("E2", CultureInfo.InvariantCulture);
                var sParts = sci.Split('E');
                lbl.Text = $"{sParts[0]} x 10^{sParts[1]}";
            }
            else lbl.Text = "OFF";

            return pTorr;
        }

        private void AddPointToSeries(LineSeries s, double x, double y)
        {
            if (y <= 0 || double.IsNaN(y)) y = double.NaN;
            s.Points.Add(new DataPoint(x, y));
            if (s.Points.Count > MAX_POINTS) s.Points.RemoveAt(0);
        }

        #endregion

        #region Buttons (View, Set, Log)

        private void View_btn_Click(object sender, EventArgs e)
        {
            isAutoScroll = !isAutoScroll;
            UpdateViewButtonState();
        }

        private void UpdateViewButtonState()
        {
            View_btn.Text = isAutoScroll ? "Live" : "History";
            View_btn.BackColor = isAutoScroll ? Color.LightGreen : Color.Gold;
        }

        private void SetLim_btn_Click(object sender, EventArgs e)
        {
            UpdateLimitsFromTextBoxes();
            MessageBox.Show("Alarm limits updated!");
        }

        private void logging_btn_Click(object sender, EventArgs e)
        {
            if (!isLogging)
            {
                try
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "VacuumLogs");
                    Directory.CreateDirectory(path);
                    string file = Path.Combine(path, $"VacuumData_{DateTime.Now:yyyy-MM-dd}.csv");
                    bool newFile = !File.Exists(file) || new FileInfo(file).Length == 0;

                    logWriter = new StreamWriter(file, true, Encoding.UTF8) { AutoFlush = true };
                    if (newFile) logWriter.WriteLine("Date,Time,Gun,Linac,Dipole");
                    else logWriter.WriteLine("--- Resumed ---");

                    isLogging = true;
                    Trigger_Timer.Interval = 200;
                    Logging_btn.Text = "Stop Logging";
                    Logging_btn.BackColor = Color.LightGreen;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                StopLogging();
                Trigger_Timer.Interval = 500;
                Logging_btn.Text = "Start Logging";
                Logging_btn.BackColor = SystemColors.Control;
            }
        }

        private void StopLogging()
        {
            isLogging = false;
            if (logWriter != null) { logWriter.Close(); logWriter = null; }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Trigger_Timer.Enabled = false;
            StopLogging();
            if (ticHelper != null) try { ticHelper.Disconnect(); } catch { }
        }

        #endregion
    }
}
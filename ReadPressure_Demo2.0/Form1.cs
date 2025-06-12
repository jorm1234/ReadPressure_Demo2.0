using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TICMonitorApp;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.IO.Ports;
// Removed unused 'Microsoft.SqlServer.Server' - if you need it, add it back.
// using Microsoft.SqlServer.Server;

namespace ReadPressure_Demo2._0
{
    public partial class Form1 : Form
    {
        private TICSerialHelper ticHelper;

        private PlotModel plotModel;
        private LineSeries lineSeries;

        private const double PASCAL_TO_TORR_CONVERSION = 133.322; // Added constant for conversion
        private const double HISTORY_SECONDS = 90; // Display data for the last 60 seconds

        public Form1()
        {
            InitializeComponent();
            RefreshComPorts();
            InitializePlot();
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

        private void InitializePlot()
        {
            plotModel = new PlotModel { Title = "Pressure vs Time" };
            lineSeries = new LineSeries
            {
                Title = "Pressure (Torr)",
                MarkerType = MarkerType.None,
                Color = OxyColors.Blue,         // Explicitly set a visible color
                StrokeThickness = 2             // Make the line a bit thicker
            };

            plotModel.Series.Add(lineSeries);

            // Changed to DateTimeAxis for time on the X-axis
            plotModel.Axes.Add(new OxyPlot.Axes.DateTimeAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Title = "Time (Local)",
                StringFormat = "HH:mm:ss", // Format for Hour:Minute:Second
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IntervalType = OxyPlot.Axes.DateTimeIntervalType.Seconds, // Suggests labels for seconds
                MajorGridlineColor = OxyColor.FromArgb(40, 0, 0, 0) // Lighter gridlines
            });

            plotModel.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Title = "Pressure (Torr)",
                // Removed Minimum and Maximum to allow auto-scaling initially
                // Minimum = 1e-10,
                // Maximum = 1e-1,
                IsZoomEnabled = true,
                IsPanEnabled = true,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                TitleFontSize = 12,
                UseSuperExponentialFormat = true,
                StringFormat = "0.##E+0",
                StartPosition = 1,
                EndPosition = 0,
                MajorGridlineColor = OxyColor.FromArgb(40, 0, 0, 0) // Lighter gridlines
            });

            PressurePlot.Model = plotModel;
        }

        private void send_btn_Click_1(object sender, EventArgs e)
        {
            Trigger_Timer.Enabled = true;
        }

        private void stop_btn_Click_1(object sender, EventArgs e)
        {
            Trigger_Timer.Enabled = false;
        }

        private void Trigger_Timer_Tick_1(object sender, EventArgs e)
        {
            // IMPORTANT: Consider moving connection logic out of the timer tick as suggested in previous responses.
            // If connection fails repeatedly here, it can be annoying for the user.
            if (ticHelper == null || !ticHelper.IsConnected)
            {
                string selectedPort = ComPort_cbx.SelectedItem as string;

                if (string.IsNullOrEmpty(selectedPort) || selectedPort == "No COM ports found")
                {
                    Trigger_Timer.Enabled = false;
                    MessageBox.Show("Please select a valid COM port.");
                    return;
                }

                ticHelper = new TICSerialHelper(selectedPort);
                if (!ticHelper.Connect())
                {
                    Trigger_Timer.Enabled = false;
                    MessageBox.Show("Failed to connect to TIC on " + selectedPort);
                    return;
                }
            }

            string response = ticHelper.SendCommand("?V913");

            if (response.StartsWith("=V913"))
            {
                try
                {
                    string[] parts = response.Split(' ')[1].Split(';');
                    double pressurePa = double.Parse(parts[0]);
                    double pressureTorr = pressurePa / PASCAL_TO_TORR_CONVERSION; // Using the constant

                    // Check for invalid numbers before plotting
                    if (double.IsNaN(pressureTorr) || double.IsInfinity(pressureTorr))
                    {
                        Pressure_label.Text = "Invalid Data";
                        // Optionally, log this error or skip adding the point
                        return;
                    }

                    Pressure_label.Text = pressureTorr.ToString("E2") + " Torr";

                    // Plot update with current local time
                    DateTime currentTime = DateTime.Now;
                    lineSeries.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(currentTime), pressureTorr));

                    // Remove old points to maintain a sliding time window (e.g., last 60 seconds)
                    double minTime = OxyPlot.Axes.DateTimeAxis.ToDouble(currentTime.AddSeconds(-HISTORY_SECONDS));

                    // Remove points from the beginning of the series that are older than the window
                    while (lineSeries.Points.Count > 0 && lineSeries.Points[0].X < minTime)
                    {
                        lineSeries.Points.RemoveAt(0);
                    }

                    // Auto-adjust the X-axis (time axis) to show the last HISTORY_SECONDS
                    plotModel.Axes[0].Minimum = OxyPlot.Axes.DateTimeAxis.ToDouble(currentTime.AddSeconds(-HISTORY_SECONDS));
                    plotModel.Axes[0].Maximum = OxyPlot.Axes.DateTimeAxis.ToDouble(currentTime.AddSeconds(5)); // A little buffer for future data

                    plotModel.InvalidatePlot(true); // Redraw the plot
                }
                catch (FormatException) // Catch specific parsing error for better handling
                {
                    Pressure_label.Text = "Parse Error";
                }
                catch (IndexOutOfRangeException) // Catch if parts array is too small
                {
                    Pressure_label.Text = "Response Format Error";
                }
                catch (Exception ex) // General fallback for other issues
                {
                    Pressure_label.Text = $"Error: {ex.Message}";
                }
            }
            else
            {
                Pressure_label.Text = response; // Display the raw response if it doesn't start with "=V913"
            }
        }

        private void reCom_btn_Click(object sender, EventArgs e)
        {
            RefreshComPorts();
        }
    }
}
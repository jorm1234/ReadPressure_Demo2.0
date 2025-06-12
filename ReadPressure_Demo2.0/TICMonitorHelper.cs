using System;
using System.IO.Ports;

namespace TICMonitorApp
{
    public class TICSerialHelper
    {
        private SerialPort serialPort;

        public bool IsConnected => serialPort != null && serialPort.IsOpen;

        public TICSerialHelper(string portName, int baudRate = 9600)
        {
            serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
            serialPort.NewLine = "\r";
            serialPort.ReadTimeout = 500;  // milliseconds
        }

        public bool Connect()
        {
            try
            {
                if (!serialPort.IsOpen)
                    serialPort.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Disconnect()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        public string SendCommand(string command)
        {
            if (!IsConnected)
                throw new InvalidOperationException("Serial port is not connected.");

            try
            {
                serialPort.WriteLine(command);               // e.g., "?V913"
                string response = serialPort.ReadLine();     // expects CR terminated response
                return response.Trim();
            }
            catch (TimeoutException)
            {
                return "ERROR: Timeout";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }
    }
}

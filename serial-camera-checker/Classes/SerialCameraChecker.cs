using System.IO.Ports;
using System.Text;

namespace SerialCameraChecker
{
    public class SerialCameraChecker : ISerialCameraChecker
    {
        private string? comPort;
        private int baudrate;
        private int dataBits;
        private Parity parity;
        private StopBits stopBits;

        public SerialCameraChecker(string? comPort, int baudrate, int dataBits, Parity parity, StopBits stopBits)
        {
            if (comPort == null)
            {
                this.comPort = SerialPort.GetPortNames()[0];
            }
            else
            {
                this.comPort = comPort;
            }
            this.baudrate = baudrate;
            this.dataBits = dataBits;
            this.parity = parity;
            this.stopBits = stopBits;

            OpenPort(ConnectToPort(this.comPort));
            Console.WriteLine($"Connected to {GetPortInfo(ConnectToPort(this.comPort))}");
        }

        public bool IsCameraConnected(SerialPort serialPort)
        {
            return serialPort.IsOpen;
        }

        public SerialPort ConnectToPort(string portName)
        {
            SerialPort serialPort = new(portName)
            {
                BaudRate = baudrate,
                DataBits = dataBits,
                Parity = parity,
                StopBits = stopBits
            };
            return serialPort;
        }

        public void OpenPort(SerialPort serialPort)
        {
            try
            {
                serialPort.Open();
            }
            catch (System.Exception)
            {
                
                throw new System.Exception("Failed to open port, please check that the port is available and try again.");
            }
            
        }

        public string[] GetPorts()
        {
            throw new System.NotImplementedException();
        }

        public string GetPortInfo(SerialPort serialPort)
        {
            StringBuilder portInfo = new();
            portInfo.Append($"Port Name: {serialPort.PortName}\n");
            portInfo.Append($"Baudrate: {serialPort.BaudRate}\n");
            portInfo.Append($"Data Bits: {serialPort.DataBits}\n");
            portInfo.Append($"Parity: {serialPort.Parity}\n");
            portInfo.Append($"Stop Bits: {serialPort.StopBits}\n");
            return portInfo.ToString();
        }

        public bool WriteToPort(SerialPort serialPort, string message)
        {
            throw new System.NotImplementedException();
        }

        public void ReadFromPort(SerialPort serialPort)
        {
            throw new System.NotImplementedException();
        }
    }
}
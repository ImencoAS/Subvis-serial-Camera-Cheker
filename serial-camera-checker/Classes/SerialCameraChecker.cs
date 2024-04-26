using System.IO.Ports;
using System.Text;

namespace SerialCameraChecker
{
    public class SerialCameraChecker(int baudrate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One) : ISerialCameraChecker
    {
        public bool IsCameraConnected(SerialPort serialPort)
        {
            throw new System.NotImplementedException();
        }

        public SerialPort ConnectToPort(string portName)
        {
            throw new System.NotImplementedException();
        }

        public void OpenPort(SerialPort serialPort)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetPorts()
        {
            throw new System.NotImplementedException();
        }

        public string GetPortInfo(SerialPort serialPort)
        {
            throw new System.NotImplementedException();
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
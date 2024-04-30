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
                Console.WriteLine("No COM port specified, using the first available port. " + SerialPort.GetPortNames()[0]);
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

            SerialPort serialPort = ConnectToPort(this.comPort);

            if(IsCameraConnected(serialPort))
            {
                Console.WriteLine("Camera is connected.\n" + GetPortInfo(serialPort));
                Thread newThread = new(() => ReadFromPort(serialPort));
                newThread.Start();
                WriteToPort(serialPort, "81090435ff"); // Send command to camera
            }
            else
            {
                Console.WriteLine("Camera is not connected.");
            }
        }

        public bool IsCameraConnected(SerialPort serialPort)
        {
            return serialPort.IsOpen;
        }

        public SerialPort ConnectToPort(string portName)
        {
            SerialPort serialPort = new(portName)
            {
                BaudRate = this.baudrate,
                DataBits = this.dataBits,
                Parity = this.parity,
                StopBits = this.stopBits
            };
            Console.WriteLine("Connecting to port: " + portName);
            OpenPort(serialPort);
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
                Console.Error.WriteLine("Failed to open port, please check that the port is available and try again.");

            }
        }

        public string[] GetPorts()
        {
            return SerialPort.GetPortNames();
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
            byte[] messageBytes = StringToByteArray(message);
            serialPort.Write(messageBytes, 0, messageBytes.Length);
            return true;
        }

public void ReadFromPort(SerialPort serialPort)
{
            Console.WriteLine("Reading data");
            while(serialPort.IsOpen)
            {
                serialPort.DataReceived += (sender, e) =>
                {
                    string rawResponse = serialPort.ReadExisting();
                    byte[] responseData = Encoding.Default.GetBytes(rawResponse); // Convert string to byte array
                    Console.WriteLine("Hex encoding: " + ByteArrayToHexString(responseData));
                    serialPort.Close();
                };
            }
        }


        public static byte [] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            for (int i = 0; i < bytes.Length; i++)
            {
                Console.WriteLine("Byte " + i + ": " + bytes[i]);
            }
            return bytes;
        }

        public static string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder result = new(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                result.AppendFormat("{0:X2}", b);
            }
            return result.ToString();
        }

    }
}
using System.IO.Ports;

class Program
{
    static void Main(string[] args)
    {
        string? comPort;
        int baudrate = 9600;
        int dataBits = 8;
        Parity parity = Parity.None;
        StopBits stopBits = StopBits.One;

        if(args.Length == 0)
        {
            SerialCameraChecker.SerialCameraChecker serialCameraChecker = new(null, baudrate, dataBits, parity, stopBits);
        }
        else if(args[0] == "--help")
        {
            Console.WriteLine("Usage: serial-camera-checker [options]");
            Console.WriteLine("Options:");
            Console.WriteLine("--com-port <port> : Set the COM port to use");
            Console.WriteLine("--baudrate <baudrate> : Set the baudrate to use");
            Console.WriteLine("--data-bits <data-bits> : Set the data bits to use");
            Console.WriteLine("--parity <parity> : Set the parity to use");
            Console.WriteLine("--stop-bits <stop-bits> : Set the stop bits to use");
            Console.WriteLine("--list-ports : List available COM ports");
            return;
        }
        else if(args[0] == "--list-ports")
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                Console.WriteLine(port);
            }
            return;
        }
        else
        {
            // Parse command line arguments
            for (int i = 0; i < args.Length; i += 2) // Increment by 2 to get the next pair of arguments
            {
                switch (args[i])
                {
                    case "--com-port":
                        comPort = args[i + 1];
                        break;
                    case "--baudrate":
                        baudrate = int.Parse(args[i + 1]);
                        break;
                    case "--data-bits":
                        dataBits = int.Parse(args[i + 1]);
                        break;
                    case "--parity":
                        parity = (Parity)Enum.Parse(typeof(Parity), args[i + 1]);
                        break;
                    case "--stop-bits":
                        stopBits = (StopBits)Enum.Parse(typeof(StopBits), args[i + 1]);
                        break;
                    default:
                        Console.WriteLine($"Unknown argument: {args[i]}");
                        break;
                }
            }
        }
    }
}
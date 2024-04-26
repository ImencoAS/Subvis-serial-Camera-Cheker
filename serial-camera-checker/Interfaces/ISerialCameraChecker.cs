using System.IO.Ports;

interface ISerialCameraChecker
{
    bool IsCameraConnected(SerialPort serialPort);
    SerialPort ConnectToPort(string portName);
    void OpenPort(SerialPort serialPort);
    string[] GetPorts();
    string GetPortInfo(SerialPort serialPort);
    
    /*
    *I/O operation
    */
    bool WriteToPort(SerialPort serialPort, string message);
    /*
    *I/O operation
    */
    void ReadFromPort(SerialPort serialPort);
}
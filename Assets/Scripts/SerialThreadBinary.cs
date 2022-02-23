using UnityEngine;
using System.IO.Ports;

public class SerialThreadBinary : AbstractSerialThread
{
    public SerialThreadBinary(string portName,
                                       int baudRate,
                                       int delayBeforeReconnecting,
                                       int maxUnreadMessages,
                                       bool dropOldMessage)
        : base(portName, baudRate, delayBeforeReconnecting, maxUnreadMessages, false, dropOldMessage)
    {

    }

    protected override void SendToWire(object message, SerialPort serialPort)
    {
        // Coloca aquí tu código
    }

    protected override object ReadFromWire(SerialPort serialPort)
    {
        // Coloca aquí tu código
    }
}

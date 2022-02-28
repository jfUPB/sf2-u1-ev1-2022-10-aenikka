using System;
using UnityEngine;
using System.IO.Ports;

public class SerialThreadBinary : AbstractSerialThread
{
    private byte[] buffer = new byte[12];
    private int state = 0;
    private int bytesExpect = 0;

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
        byte[] binaryMessage = (byte[])message;
        serialPort.Write(binaryMessage, 0, binaryMessage.Length);
    }

    protected override object ReadFromWire(SerialPort serialPort)
    {
        byte[] buff = null; 
        
        if (state == 0)
        {
            if (serialPort.BytesToRead > 0)
            {
                state = 1;
                serialPort.Read(buffer, 0, 1);
                bytesExpect = Convert.ToInt32(buffer[0]);
            }
        }

        if (state == 1)
        {
            if (serialPort.BytesToRead > bytesExpect-1)
            {
                serialPort.Read(buffer, 0, bytesExpect);
                byte[] returnBuffer = new byte[bytesExpect];
                System.Array.Copy(buffer, returnBuffer, bytesExpect);
                state = 0;
                buff = returnBuffer;
                //return returnBuffer;
            }
        }

        return buff;

    }
}

/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;

using System.IO.Ports;

/**
 * This class contains methods that must be run from inside a thread and others
 * that must be invoked from Unity. Both types of methods are clearly marked in
 * the code, although you, the final user of this library, don't need to even
 * open this file unless you are introducing incompatibilities for upcoming
 * versions.
 * 
 * For method comments, refer to the base class.
 */
public class SerialThreadASCII : AbstractSerialThread
{
    public SerialThreadASCI(string portName,
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

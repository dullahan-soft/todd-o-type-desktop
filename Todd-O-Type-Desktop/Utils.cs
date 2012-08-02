using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Todd_O_Type_Desktop
{
    class Utils
    {
        public const int BAUDRATE = 9600;
        /* opens a serial port */
        public static SerialPort openPort(String portName)
        {
            SerialPort port = null;
            try
            {
                port = new SerialPort(portName, BAUDRATE);
                port.Open();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return port;
        }

        /* only closes a serial port if it is valid */
        static public void safePortClose(SerialPort p)
        {
            if (isPortValid(p))
            {
                p.Close();
            }
        }

        /* tests if a port is not null and open */
        static public bool isPortValid(SerialPort p)
        {
            if (p != null && p.IsOpen)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

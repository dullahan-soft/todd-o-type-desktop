using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using CommandLine;
using System.Net;
using System.IO;

namespace Todd_O_Type_Desktop
{
    class Program
    {
        private const string DEFAULT_SECRET = "9wk3u5d";
        private const string URL = "https://desolate-eyrie-6214.herokuapp.com/stations/kdvs";

        /* serial port used to communicate with the arduino */
        private static SerialPort curPort;

        /* secret used to communicate with the playlist server */
        private static string curSecret;

        static int Main(string[] args)
        {
            /* holds command line options */
            var options = new Options();

            string arduinoInput;
            DateTime timestamp;
            WebRequest request;
            string post;
            byte[] byteArray;

            /* parse and process command line options */
            if (CommandLineParser.Default.ParseArguments(args, options))
            {
                if (options.Port != null)
                {
                    curPort = Utils.openPort(options.Port);
                }
                else
                {
                    Console.WriteLine("\nBad port.\n");
                    return 1;
                }

                if (options.Secret != null)
                {
                    curSecret = options.Secret;
                }
                else
                {
                    curSecret = DEFAULT_SECRET;
                }
            }
            while(true)
            {
                Thread.Sleep(10);
                
                if (Utils.isPortValid(curPort) && curPort.BytesToRead > 0)
                {
                    timestamp = new DateTime();
                    arduinoInput = curPort.ReadLine();
                    post = timestamp + " : " + arduinoInput.Trim();
                    byteArray = Encoding.UTF8.GetBytes(post);

                    Console.WriteLine(post);
                    
                    request = WebRequest.Create(URL);
                    request.Method = "POST";
                    request.ContentLength = byteArray.Length;
                    request.ContentType = "application/x-www-form-urlencoded";

                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
            }
        }
    }
}

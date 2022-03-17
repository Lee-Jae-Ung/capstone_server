using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace capstone_server
{
    public class Program
    {
        public static int buffsize = 1024;
        static SerialPort _serialPort;
        public static double[] ts = new double[buffsize];

        public static void Main(string[] args)
        {


            SerialOpen();

            Thread serial = new Thread(() => Read());
            Thread svstart = new Thread(() => CreateHostBuilder(args).Build().Run());

            serial.Start();
            svstart.Start();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://0.0.0.0:50010");
                    webBuilder.UseStartup<Startup>();
                });


        public static void SerialOpen()
        {
            _serialPort = new SerialPort();

            _serialPort.PortName = "COM5";
            _serialPort.BaudRate = 115200;
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            _serialPort.Open();
        }

        public static void Read()
        {

            int seq = 0;

            //Thread.Sleep(1000);
            while (true)
            {
                //Thread.Sleep(1000);

                try
                {
                    //Console.WriteLine("반복문 시작");

                    string message = _serialPort.ReadLine();
                    //Console.WriteLine(message);
                    //Console.WriteLine(double.Parse(message));

                    while (seq < buffsize)
                    {
                        //Console.WriteLine("while 2번째");

                        ts[seq] = double.Parse(message);
                        seq++;
                    }

                    seq = 0;

                    //Console.WriteLine(ts);

                }

                catch (TimeoutException)
                {
                }


            }
        }
    }
}

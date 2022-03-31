using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
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


            //SerialOpen();
            //Thread serial;
            Thread svstart;

            // serial = new Thread(() => Read());
            //serial.Start();



            svstart = new Thread(() => CreateHostBuilder(args).Build().Run());
            svstart.Start();




        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://0.0.0.0:50010");
                    webBuilder.UseStartup<Startup>();
                });


        public static void SerialSearch()
        {
            string[] PortNames = SerialPort.GetPortNames();

            foreach (string portnumber in PortNames)
            {
                Console.WriteLine(portnumber);
            }
        }

        public static void SerialOpen()
        {
            _serialPort = new SerialPort();

            Console.Write("input port : ");

            _serialPort.PortName = Console.ReadLine();
            _serialPort.BaudRate = 115200;
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            try
            {
                _serialPort.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("포트의 연결상태를 확인하십시오");

            }
        }

        public static void Read()
        {
            int seq = 0;

            while (true)
            {
                try
                {

                    string message;
                    while (seq < buffsize)
                    {
                        message = _serialPort.ReadLine();
                        ts[seq] = double.Parse(message);
                        seq++;
                    }
                    seq = 0;
                }
                catch (TimeoutException) { }
            }

        }

        
    }
    public class Manager
    {
        public static double t;
        public static string GetInfo()
        {

            PerformanceCounter cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            // will always start at 0
            dynamic firstValue = cpuCounter.NextValue();
            Thread.Sleep(1000);
            // now matches task manager reading
            dynamic cpuUsage = cpuCounter.NextValue();

            //Console.WriteLine(secondValue);

            ManagementClass cls = new("Win32_OperatingSystem");
            ManagementObjectCollection instances = cls.GetInstances();

            double total_physical_memeory = 0;
            double free_physical_memeory = 0;
            double used_physical_memory = 0;


            foreach (ManagementObject info in instances)
            {
                total_physical_memeory = Math.Round(double.Parse(info["TotalVisibleMemorySize"].ToString()) / 1024, 1);
                free_physical_memeory = Math.Round(double.Parse(info["FreePhysicalMemory"].ToString()) / 1024, 1);
                used_physical_memory = total_physical_memeory - free_physical_memeory;

            }

            string status_info = "{'cpu'" + ":" + "'" + Math.Round(cpuUsage, 1).ToString() + " %" + "'," +
                "'ram_total'" + ":" + "'" + total_physical_memeory.ToString() + " MB" + "'," +
                "'ram_usage'" + ":" + "'" + Math.Round(used_physical_memory, 1).ToString() + " MB" + "'," +
                "'ram_usage_per'" + ":" + "'" + Math.Round(used_physical_memory * 100 / total_physical_memeory, 1).ToString() + " %" + "'}";
            return status_info;
        }
        public static void Reset()
        {
            Process.Start("shutdown.exe", "-r");
        }


    }
}

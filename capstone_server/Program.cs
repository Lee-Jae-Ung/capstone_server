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
using static capstone_server.DataStructure;


namespace capstone_server
{
    public class Program
    {
        public static int buffsize = 51200;
        static SerialPort _serialPort;
        public static double[] ts = new double[buffsize];

        static int count123 = 0;


        //public static DateTime dtime = new DateTime(2021, 04, 14, 14, 00, 00);
        //public static DateTime dtime_now = DateTime.Now.AddYears(-1);

        //public static string t = "";
        //public static string t2 = "";


        public static void Main(string[] args)
        {

            //Console.WriteLine(dtime_now);

            //Thread timer;
            /*
            timer = new Thread(() => Timer12());
            timer.Start();

            serial = new Thread(() => ReadTest());
            serial.Start();
            */

            //dtime = dtime.AddSeconds(1.0);


            /*
            Thread AE_1_1;
            AE_1_1 = new Thread(() => runDevice("1","145", ref AEdata_1_1));
            AE_1_1.Start();

            Thread AE_1_2;
            AE_1_2 = new Thread(() => runDevice("1", "146",ref AEdata_1_2));
            AE_1_2.Start();

            Thread VIB_1_1;
            VIB_1_1 = new Thread(() => runDevice("1", "149",ref VIBdata_1_1));
            VIB_1_1.Start();

            Thread VIB_1_2;
            VIB_1_2 = new Thread(() => runDevice("1", "150",ref VIBdata_1_2));
            VIB_1_2.Start();
            */
            /*
            SerialOpen();
            Thread serial;
            serial = new Thread(() => Read());
            serial.Start();
            */
            /*
            Thread clock;
            clock = new Thread(() => startclock());
            clock.Start();
            */
            /*
            Thread clock2;
            clock2 = new Thread(() => startDayclock());
            clock2.Start();
            */

            Thread svstart;
            svstart = new Thread(() => CreateHostBuilder(args).Build().Run());
            svstart.Start();




        }
        /*
        public static void startclock()
        {
            
            while (true)
            {
                
                Thread.Sleep(1000);
                t = dtime.ToString("yyyy-MM-dd HH:mm:ss");
                dtime = dtime.AddMinutes(5.0);
                t2 = dtime.ToString("yyyy-MM-dd HH:mm:ss");
                //Console.WriteLine(t);
                //Console.WriteLine(t2);

            }
        }

        public static void startDayclock()
        {
            while (true)
            {
                Thread.Sleep(1000);
                t2 = dtime.ToString("yyyy-MM-dd HH:mm:ss");
                dtime = dtime.AddDays(-1.0);
                t = dtime.ToString("yyyy-MM-dd HH:mm:ss");
                //Console.WriteLine(t);
                //Console.WriteLine(t2);

            }
        }
        
        public static void runDevice(string facility_id, string point_id,ref SDG_F fdata)
        {

            while (true)
            {
                Thread.Sleep(1000);

                dtime = dtime.AddSeconds(1.0);
                string t = dtime.ToString("yyyy-MM-dd HH:mm:ss");
                //Console.WriteLine(t);
                //fdata = ManageDb.SelectAE(point_id, t);

            }
        }
        */


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

        public static void ReadTest()
        {
          

            while (true)
            {
                try
                {

                    string message;
                    
                    
                    message = _serialPort.ReadLine();
                    count123++;
                    
                }
                catch (TimeoutException) { }
            }

        }

        public static void Timer12()
        {


            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine(count123);
                count123 = 0;
            }

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


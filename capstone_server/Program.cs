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
        static SerialPort _serialPort;
        public static string daq_data = null;
        static bool status = true;

        public static void Main(string[] args)
        {

          
            
            SerialOpen();
            if (status)
            {
                Thread serial;
                serial = new Thread(() => Read());
                serial.Start();
            }
            
            
           

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
            Console.WriteLine("**연결된 포트가 없다면 none 을 입력하시오**");
            Console.Write("포트를 입력하시오 : ");

            _serialPort.PortName = Console.ReadLine();
            
            if (_serialPort.PortName.Equals("none"))
            {
                status = false;
                return;
            }
            else
            {
                _serialPort.BaudRate = 115200;
                _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
                _serialPort.DataBits = 8;
                _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");

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
        }

        public static void Read()
        {
            
            int seq = 0;

            while (true)
            {
                try
                {


                    
                    daq_data = _serialPort.ReadLine();

                }
                catch (TimeoutException) { }
            }

        }
        /*
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

        */
    }
}



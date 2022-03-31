using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using rest_test.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;




//test
namespace capstone_server.Controllers
{//RestClient
 //restrequest

    [Route("manage/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        [HttpGet("device0")]
        public string Sig1()
        {

            double[] tset = Program.ts;
            //double[] tset = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            double rms;
            double peak;
            double tmp_sum = 0.0;
            /*
            double[] rdt = Program.signal;

            

            avg = rdt.Average();
            std = rdt.Average();
            max = rdt.Max();
            min = rdt.Min();
            */
            /*
            using (StreamWriter file = new StreamWriter(@"C:\Temp\data.csv"))
            {
                //file.WriteLine("Ch1");

                foreach (double dt in tset)
                {
                    file.WriteLine("{0},", dt);
                }
            }
            */
            for (int i = 0; i < 1024; i++)
            {
                tmp_sum += Math.Pow(tset[i], 2);
            }

            rms = Math.Pow((tmp_sum / 1024), 0.5);
            peak = tset.Max();

            //Console.WriteLine("tmp_sum : " + tmp_sum);
            //Console.WriteLine("피크 : "+tset.Max());
            //Console.WriteLine("rms : "+rms);
            //Console.WriteLine(tset.Average());

            string status_info = "{\"RMS\"" + ":" + "\"" + 1 + "\"," +
            "\"PEAK\"" + ":" + "\"" + 1 + "\"}";

            return status_info;
        }

        [HttpGet("device1")]
        public string Sig2()
        {

            double[] tset = Program.ts;
            //double[] tset = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            double rms;
            double peak;
            double tmp_sum = 0.0;
            /*
            double[] rdt = Program.signal;

            

            avg = rdt.Average();
            std = rdt.Average();
            max = rdt.Max();
            min = rdt.Min();
            */
            /*
            using (StreamWriter file = new StreamWriter(@"C:\Temp\data.csv"))
            {
                //file.WriteLine("Ch1");

                foreach (double dt in tset)
                {
                    file.WriteLine("{0},", dt);
                }
            }
            */
            Random random = new Random();

            random.NextDouble();
            for (int i = 0; i < 1024; i++)
            {
                tmp_sum += Math.Pow(tset[i], 2);
            }

            rms = Math.Pow((tmp_sum / 1024), 0.5);
            peak = tset.Max();

            //Console.WriteLine("tmp_sum : " + tmp_sum);
            //Console.WriteLine("피크 : "+tset.Max());
            //Console.WriteLine("rms : "+rms);
            //Console.WriteLine(tset.Average());

            string status_info = "{\"RMS\"" + ":" + "\"" + random.NextDouble() + "\"," +
            "\"PEAK\"" + ":" + "\"" + random.NextDouble() + "\"}";

            return status_info;
        }

        [HttpGet("device2")]
        public string Sig3()
        {

            double[] tset = Program.ts;
            //double[] tset = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            double rms;
            double peak;
            double tmp_sum = 0.0;
            /*
            double[] rdt = Program.signal;

            

            avg = rdt.Average();
            std = rdt.Average();
            max = rdt.Max();
            min = rdt.Min();
            */
            /*
            using (StreamWriter file = new StreamWriter(@"C:\Temp\data.csv"))
            {
                //file.WriteLine("Ch1");

                foreach (double dt in tset)
                {
                    file.WriteLine("{0},", dt);
                }
            }
            */
            for (int i = 0; i < 1024; i++)
            {
                tmp_sum += Math.Pow(tset[i], 2);
            }

            rms = Math.Pow((tmp_sum / 1024), 0.5);
            peak = tset.Max();

            //Console.WriteLine("tmp_sum : " + tmp_sum);
            //Console.WriteLine("피크 : "+tset.Max());
            //Console.WriteLine("rms : "+rms);
            //Console.WriteLine(tset.Average());

            string status_info = "{\"RMS\"" + ":" + "\"" + 100 + "\"," +
            "\"PEAK\"" + ":" + "\"" + 100 + "\"}";

            return status_info;
        }


        //CSV
    }
}
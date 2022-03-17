using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using rest_test.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;




//test
namespace capstone_server.Controllers
{//RestClient
 //restrequest

    [Route("manage/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {


        [HttpGet("info")]
        public string Stat()
        {
            try
            {
                return Manager.GetInfo();
            }
            catch (Exception e)
            {
                //JsonObject tst = new JsonObject();
                //tst.Add("msg", e.StackTrace);
                string tst = e.StackTrace;
                return tst;
            }
        }

        [HttpGet("reset")]
        public void Reset()
        {
            Manager.Reset();
        }

        [HttpGet("feature")]
        public string Sig()
        {
            double[] tset = Program.ts;
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



            for (int i = 0; i < Program.buffsize; i++)
            {
                tmp_sum += Math.Pow(tset[i], 2);
            }

            rms = Math.Pow((tmp_sum), 0.5);
            peak = tset.Max();

            //Console.WriteLine(tset.Max());
            //Console.WriteLine(rms);
            //Console.WriteLine(tset.Average());

            string status_info = "{\"RMS\"" + ":" + "\"" + rms + "\"," +
            "\"PEAK\"" + ":" + "\"" + peak + "\"}";

            return status_info;
        }


        //CSV
        [HttpGet("{filename}")]
        public async Task<IActionResult> GetDownloadResult(string filename)
        {

            var path = Directory.GetCurrentDirectory() + @"\" + filename;

            //var path = @"C:\Users\user\source\repos\restapi_server\net5.0\" + filename;

            //double[] ts = Program.signal;
            //var path = @"C:\Users\user\source\repos\NII_Test\NII_Test\RawData.db";


            

            if (System.IO.File.Exists(path))
            {
                byte[] bytes;
                using (FileStream file = new FileStream(path: path, mode: FileMode.Open)) // 배포환경에선 다운로드폴더에 대한 권한설정작업이 필요할 수 있다.
                {
                    try
                    {
                        bytes = new byte[file.Length];
                        await file.ReadAsync(bytes);
                        return File(bytes, "application/octet-stream");
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
            }
            else
            {
                return NotFound();
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


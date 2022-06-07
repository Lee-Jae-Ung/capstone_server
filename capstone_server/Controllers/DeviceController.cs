using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using rest_test.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static capstone_server.DataStructure;



//test
namespace capstone_server.Controllers
{//RestClient
 //restrequest

    [Route("manage/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        public static SDG_F feature1 = new SDG_F();
       

        [HttpGet("daqtest")]
        public string DaqTest()
        {

            string daq = Program.daq_data;


            string status_info = "{\"RMS\"" + ":" + "\"" + daq + "\"," +
            "\"DATE\"" + ":" + "\"" + "_" + "\"" + "}";
            return status_info;
        }
        /*
        [HttpGet("tttest")]
        public async Task<IActionResult> GetDownloadResult2()
        {

            
            dtime_now = DateTime.Now.AddYears(-1);
            dtime_now = dtime_now.AddHours(-1.0);
            string t2 = dtime_now.ToString("yyyy-MM-dd HH:mm:ss");
            dtime_now = dtime_now.AddDays(-1.0);
            string t = dtime_now.ToString("yyyy-MM-dd HH:mm:ss");
            dtime_now = dtime_now.AddMinutes(1.0);
            
            DateTime now_d = new DateTime();
            DateTime now_d_day = new DateTime();
            DateTime now_d_week = new DateTime();
            DateTime now_d_month = new DateTime();
            
            DateTime now_date = DateTime.Now.AddYears(-1);

            AE_F feature = new AE_F();
            feature = ManageDb.SelectAE_day("207", now_date,"hour");

            double[] rms1 = feature1.RMS1_arr;
            double[] rms2 = feature1.RMS2_arr;
            string[] date2 = feature1.date2;
            int count = feature1.count;


            var path = Directory.GetCurrentDirectory() + @"\test.csv";


            


            

            //var path = @"C:\Users\user\source\repos\restapi_server\net5.0\" + filename;

            //var path = @"C:\Users\user\source\repos\NII_Test\NII_Test\RawData.db";

            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine("csv,"+"hour"+",");
                file.WriteLine("date,rms1,rms2,");

                
                for(int i = 0; i < count; i++)
                {
                    file.Write("{0},", date2[i]);
                    file.Write("{0},", rms1[i]);
                    file.Write("{0},", rms2[i]);
                }
                
            }

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
        */
        [HttpGet("{filename}/{period}")]
        public async Task<IActionResult> GetDownloadResult(string filename, string period)
        {

            string facility_id = "";

            DateTime now_date = DateTime.Now.AddYears(-1);
            string t2 = now_date.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime cmp_date = new DateTime();

            string t = "";
            if (period.Equals("hour"))
            {
                cmp_date = now_date.AddHours(-1.0);
                t = cmp_date.ToString("yyyy-MM-dd HH:mm:ss");

            }
            else if (period.Equals("day"))
            {
                cmp_date = now_date.AddDays(-1.0);
                t = cmp_date.ToString("yyyy-MM-dd HH:mm:ss");


            }
            else if (period.Equals("week"))
            {
                cmp_date = now_date.AddDays(-7.0);
                t = cmp_date.ToString("yyyy-MM-dd HH:mm:ss");

            }
            else if (period.Equals("month"))
            {
                cmp_date = now_date.AddMonths(-1);
                t = cmp_date.ToString("yyyy-MM-dd HH:mm:ss");

            }

            else if (period.Equals("year"))
            {
                cmp_date = now_date.AddYears(-1);
                t = cmp_date.ToString("yyyy-MM-dd HH:mm:ss");

            }





            if (filename.Contains("1"))
            {
                facility_id = "1";
            }

            else if (filename.Contains("2"))
            {
                facility_id = "2";
            }

            else if (filename.Contains("3"))
            {
                facility_id = "3";
            }

            else if (filename.Contains("4"))
            {
                facility_id = "4";
            }

            feature1 = ManageDb.SelectSDG_period(facility_id, t, t2);

            double[] rms1 = feature1.RMS1_arr;
            double[] rms2 = feature1.RMS2_arr;
            double[] rms3 = feature1.RMS3_arr;
            double[] rms4 = feature1.RMS4_arr;
            string[] date2 = feature1.date2;
            int count = feature1.count;


            var path = Directory.GetCurrentDirectory() + @"\" + filename;





            var log_path = Directory.GetCurrentDirectory() + @"\accesslog_" + now_date.ToString("yyyy") + now_date.ToString("MM") + now_date.ToString("dd") + ".txt";

            using (StreamWriter log_file = new StreamWriter(log_path, append: true))
            {

                log_file.WriteLine("access_time  :  " + t2);
                log_file.WriteLine("facility id  :  " + facility_id);
                log_file.WriteLine("request period  :  " + period);
                log_file.WriteLine("start date  :  " + t);
                log_file.WriteLine("end date  :  " + t2);
                log_file.WriteLine("FILE PATH  :  " + path);
                log_file.WriteLine("ROW COUNT  :  " + count);
                log_file.WriteLine("-----------------------------");


            }

            Console.WriteLine("-----------------------------");
            Console.WriteLine("facility id     :  " + facility_id);
            Console.WriteLine("request period  :  " + period);
            Console.WriteLine("start date      :  " + t);
            Console.WriteLine("end date        :  " + t2);
            Console.WriteLine("FILE PATH       :  " + path);
            Console.WriteLine("ROW COUNT       :  " + count);
            Console.WriteLine("-----------------------------");



            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine("csv," + period + ",");
                file.WriteLine("date,rms1,rms2,rms3,rms4,");


                for (int i = 0; i < count; i++)
                {
                    file.Write("{0},", date2[i]);
                    file.Write("{0},", rms1[i]);
                    file.Write("{0},", rms2[i]);
                    file.Write("{0},", rms3[i]);
                    file.Write("{0},\n", rms4[i]);
                }

            }

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
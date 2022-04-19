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

        public static DateTime dtime_now = new DateTime();

        public static SDG_F feature = new SDG_F();

        public static SDG_F feature1 = new SDG_F();
        public static SDG_F feature2 = new SDG_F();
        public static SDG_F feature3 = new SDG_F();
        public static SDG_F feature4 = new SDG_F();

        /*
        [HttpGet("AE{pointid}")]
        public string Sig1(string pointid)
        {

            //Console.WriteLine("AE진입");
            //Console.WriteLine(t);
            feature = ManageDb.SelectAE(pointid, Program.t);

            double amplitude = feature.Amplitude;
            double rms = feature.RMS;


            string status_info = "{\"AMPLITUDE\"" + ":" + "\"" + Math.Round(amplitude, 6).ToString() + "\"," +
            "\"RMS\"" + ":" + "\"" + Math.Round(rms, 6).ToString()  + "\"}";

            return status_info;
        }
        */
        [HttpGet("VIB{facilityid}")]
        public string Sig2(string facilityid)
        {
            string t = dtime_now.ToString("yyyy-MM-dd HH:mm:ss");
            dtime_now = dtime_now.AddMinutes(5.0);
            string t2 = dtime_now.ToString("yyyy-MM-dd HH:mm:ss");
            /*
            //Console.WriteLine("VIB진입");

            feature = ManageDb.SelectAE(pointid, Program.t);


            double hrms = feature.High_RMS;
            double erms = feature.ECU_RMS;
            double irms = feature.ISO_RMS_speed;

            string status_info = "{\"HIGHRMS\"" + ":" + "\"" + Math.Round(hrms, 6).ToString() + "\"," +
            "\"ECURMS\"" + ":" + "\"" + Math.Round(erms, 6).ToString() + "\"," +
            "\"ISORMS_speed\"" + ":" + "\"" + Math.Round(irms, 6).ToString() + "\"}";

            return status_info;
            */

            Console.WriteLine("realtime"+dtime_now);


            feature = ManageDb.SelectSDG(facilityid, t,t2);

            double rms1 = feature.RMS_1;
            double rms2 = feature.RMS_2;
            double rms3 = feature.RMS_3;
            double rms4 = feature.RMS_4;
            string date = feature.date;


            //var path = @"C:\Users\user\source\repos\restapi_server\net5.0\" + filename;

            //var path = @"C:\Users\user\source\repos\NII_Test\NII_Test\RawData.db";


            //string status_info = string.Format("{\"RMS1\" : \"{0}\", \"RMS2\" : \"{1}\", \"RMS3\" : \"{2}\", \"RMS4\" : \"{3}\";",rms1,rms2,rms3,rms4);
            /*
            string status_info = "{\"HIGHRMS\"" + ":" + "\"" + Math.Round(rms1, 6).ToString() + "\"," +
            "\"ECURMS\"" + ":" + "\"" + Math.Round(rms2, 6).ToString() + "\"," +
            "\"ISORMS_speed\"" + ":" + "\"" + Math.Round(rms3, 6).ToString() + "\"}";
            */
            /*
            string status_info = "{\"RMS1\"" + ":" + "\"" + Math.Round(rms1, 6).ToString() + "\"," +
            "\"RMS2\"" + ":" + "\"" + Math.Round(rms2, 6).ToString() + "\"," +
            "\"RMS3\"" + ":" + "\"" + Math.Round(rms3, 6).ToString() + "\"," +
            "\"RMS4\"" + ":" + "\"" + Math.Round(rms4, 6).ToString() + "\"}";
            */
            string status_info = "{\"RMS1\"" + ":" + "\"" + rms1 + "\"," +
            "\"RMS2\"" + ":" + "\"" + rms2 + "\"," +
            "\"RMS3\"" + ":" + "\"" + rms3 + "\"," +
            "\"RMS4\"" + ":" + "\"" + rms4 + "\"," +
            "\"DATE\"" + ":" + "\"" + date + "\"" + "}";
            return status_info;
        }


        [HttpGet("Test{pointid}")]
        public string DaqTest(string pointid)
        {
            //특징 추가 및 시간 조정
            DateTime now_date = DateTime.Now.AddSeconds(-10);
            string t2 = now_date.ToString("yyyy-MM-dd HH:mm:ss");
            feature = ManageDb.SelectDAQ_test(pointid, t2);

            double rms1 = feature.RMS_1;
            string date = feature.date;



            string status_info = "{\"RMS1\"" + ":" + "\"" + rms1 + "\"," +
            "\"DATE\"" + ":" + "\"" + date + "\"" + "}";
            return status_info;
        }

        [HttpGet("{filename}/{period}")]
        public async Task<IActionResult> GetDownloadResult(string filename,string date,string period)
        {

            string facility_id = "";
            /*
            dtime_now = DateTime.Now.AddYears(-1);
            dtime_now = dtime_now.AddHours(-1.0);
            string t2 = dtime_now.ToString("yyyy-MM-dd HH:mm:ss");
            dtime_now = dtime_now.AddDays(-1.0);
            string t = dtime_now.ToString("yyyy-MM-dd HH:mm:ss");
            dtime_now = dtime_now.AddMinutes(1.0);
            */
            /*
            DateTime now_d = new DateTime();
            DateTime now_d_day = new DateTime();
            DateTime now_d_week = new DateTime();
            DateTime now_d_month = new DateTime();
            */
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



            Console.WriteLine("init"+date);

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

            feature1 = ManageDb.SelectSDG_day(facility_id, t,t2);

            double[] rms1 = feature1.RMS1_arr;
            double[] rms2 = feature1.RMS2_arr;
            double[] rms3 = feature1.RMS3_arr;
            double[] rms4 = feature1.RMS4_arr;
            string[] date2 = feature1.date2;
            int count = feature1.count;


            var path = Directory.GetCurrentDirectory() + @"\" + filename;

            Console.WriteLine(path);
            Console.WriteLine("count : "+count);


            //var path = @"C:\Users\user\source\repos\restapi_server\net5.0\" + filename;

            //var path = @"C:\Users\user\source\repos\NII_Test\NII_Test\RawData.db";

            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine("csv,"+period+",");
                file.WriteLine("date,rms1,rms2,rms3,rms4,");

                
                for(int i = 0; i < count; i++)
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



        /*
        [HttpGet("daq")]
        public string Sig3()
        {

            //Console.WriteLine("VIB진입");

            //feature = ManageDb.SelectAE(pointid, Program.t);


            double hrms = feature.High_RMS;
            double erms = feature.ECU_RMS;
            double irms = feature.ISO_RMS_speed;

            string status_info = "{\"HIGHRMS\"" + ":" + "\"" + Math.Round(hrms, 6).ToString() + "\"," +
            "\"ECURMS\"" + ":" + "\"" + Math.Round(erms, 6).ToString() + "\"," +
            "\"ISORMS_speed\"" + ":" + "\"" + Math.Round(irms, 6).ToString() + "\"}";

            return status_info;
        }
        */

        //CSV
    }
}
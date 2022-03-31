using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace capstone_server.Controllers
{
    [Route("manage/[controller]")]
    [ApiController]
    public class PcController : ControllerBase
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

        [HttpGet("connection")]
        public string ConnectionCheck()
        {
            return "1";
        }

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


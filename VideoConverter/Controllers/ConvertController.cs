using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VideoConverter.Model;

namespace VideoConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        // GET api/convert
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            return $"{id}";
        }

        // POST api/convert
        [HttpPost]
        public ActionResult<string> Post([FromBody] InputVideo video)
        {
            string output = "output.avi";
            string path = @"..\VideoConverter\bin\Debug\converter\";

            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = Path.Combine(path, "ffmpeg.exe");
            startInfo.Arguments = $"-y -i {path + video.VideoUrl} -vf 'scale={video.TargetSize}:-1' {path + output}";
            startInfo.RedirectStandardOutput = true;
            //startInfo.RedirectStandardError = true;

            try
            {
                using (Process process = Process.Start(startInfo))
                {
                    while (!process.StandardOutput.EndOfStream)
                    {
                        string line = process.StandardOutput.ReadLine();
                        Console.WriteLine(line);
                    }

                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return "completed";
        }

    }

    
}

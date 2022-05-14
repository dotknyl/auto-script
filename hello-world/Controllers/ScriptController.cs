using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace hello_world.Controllers
{
    [ApiController]
    [Route("api")]
    public class ScriptController : ControllerBase
    {
        private readonly ILogger<ScriptController> _logger;

        public ScriptController(ILogger<ScriptController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ls")]
        public async Task<JsonResult> Ls()
        {
            try
            {
                string results = "";
                var process = new Process();
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var processStartInfo = new ProcessStartInfo()
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = $"powershell.exe",
                        WorkingDirectory = AppContext.BaseDirectory,
                        Arguments = $"-c ls",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    };
                    process.StartInfo = processStartInfo;
                }
                else
                {
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
                }

                process.Start();

                //String error = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();

                this._logger.LogInformation(message: results);
                return new JsonResult(new { Code = "SUCCESS", Msg = results });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Code = "FAILED", Msg = ex.ToString() });
            }
        }

        [HttpGet]
        [Route("run")]
        public JsonResult Run([FromQuery] string cmd)
        {
            string results = "";
            try
            {
                var process = new Process();
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var processStartInfo = new ProcessStartInfo()
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = $"powershell.exe",
                        WorkingDirectory = AppContext.BaseDirectory,
                        Arguments = $"-c {cmd}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    };
                    process.StartInfo = processStartInfo;
                }
                else
                {
                    var processStartInfo = new ProcessStartInfo()
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = $"/bin/bash",
                        WorkingDirectory = AppContext.BaseDirectory,
                        Arguments = $"-c \"{cmd}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    };
                    process.StartInfo = processStartInfo;
                }

                process.Start();
                process.WaitForExit();

                String error = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();

                this._logger.LogInformation(message: results);
                return new JsonResult(new { Code = "SUCCESS", Msg = results, Error = error });
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
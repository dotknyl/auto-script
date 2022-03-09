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
                var processStartInfo = new ProcessStartInfo()
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = $"/bin/bash",
                    WorkingDirectory = AppContext.BaseDirectory,
                    Arguments = $"-c \"ps -aux; ls -l\"",
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
                var process = new Process();
                var processStartInfo = new ProcessStartInfo()
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = $"/bin/bash",
                    WorkingDirectory = AppContext.BaseDirectory,
                    Arguments =
                        $"-c \"chmod +x 1.sh; ./1.sh \"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit();
                
                String error = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();

                this._logger.LogInformation(message: results);
                return new JsonResult(new {Code = "SUCCESS", Msg = results, Error = error});
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
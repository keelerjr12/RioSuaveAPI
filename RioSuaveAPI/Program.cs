using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RioSuaveAPI.ProtectedJsonConfiguration;

namespace RioSuaveAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(AddProtectedJsonConfiguration)
                .UseStartup<Startup>();

        private static void AddProtectedJsonConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            var configuration = builder.Build();
            builder.AddProtectedJsonConfiguration();
        }
    }
}

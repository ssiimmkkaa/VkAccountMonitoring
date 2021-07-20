using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            _ = host.Services.GetService<HostService>();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.Sources.Clear();

                    configuration
                        .AddJsonFile("appsettings.json", optional: true,
                            reloadOnChange: true)
                        .AddJsonFile("appsettings.local.json", optional: true,
                            reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var configurationRoot = context.Configuration;
                    services
                        .Configure<VkAuthorizeData>(configurationRoot.GetSection(nameof(VkAuthorizeData)))
                        .Configure<ApplicationId>(configurationRoot.GetSection(nameof(ApplicationId)));

                    services
                        .AddSingleton<IService, VkAuthorize>()
                        .AddSingleton<HostService>();
                });
    }
}
namespace VendingTest.App
{
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using Core;
    using Core.Interfaces;
    using Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public static class Program
    {
        public static void Main()
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<VendingApplication>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddLogging(logging =>
            {
                logging.AddConfiguration(config.GetSection("Logging"));
                logging.AddConsole();
            }).Configure<LoggerFilterOptions>(options => options.MinLevel =
                LogLevel.Information);

            services.AddTransient<ICoinRepository, StaticCoinRepository>();
            services.AddTransient<IProductRepository, StaticProductRepository>();
            services.AddTransient<ICoinChecker, CoinChecker>();

            services.AddTransient<VendingApplication>();

            return services;
        }

        private static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true,
                    reloadOnChange: true);
            return builder.Build();
        }
    }
}

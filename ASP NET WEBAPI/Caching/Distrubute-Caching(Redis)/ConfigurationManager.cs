using Microsoft.Extensions.Configuration;

namespace Distrubute_Caching_Redis_
{
    public class ConfigurationManager
    {
        public static IConfiguration AppSettings { get; }

        static ConfigurationManager()
        {
            AppSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}  
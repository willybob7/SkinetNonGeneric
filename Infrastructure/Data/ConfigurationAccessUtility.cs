using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public static class ConfigurationAccessUtility
    {
        private static IConfiguration _config;
        public static IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    _config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.Development.json").Build();
                }

                return _config;
            }
        }

        /// <summary>
        /// The configurable connection string used for most database transactions.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return Configuration.GetValue<string>("ConnectionStrings:DefautConnection");
            }
        }

        public static string ApiUrl
        {
            get
            {
                return Configuration.GetValue<string>("ApiUrl");
            }
        }
    }
}

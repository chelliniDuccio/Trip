namespace Trip.Models.Extra
{
    public static class Constants
    {
        private static readonly IConfiguration config;

        static Constants()
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            GoogleApiKey = config["GoogleApiKey"] ?? throw new InvalidOperationException("GoogleApiKey is not configured.");
        }

        public static readonly string GoogleApiKey;
    }
}

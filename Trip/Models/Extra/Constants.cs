using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Trip.Models.Extra
{
    public static class Constants
    {
        private static readonly IConfiguration config;

        static Constants()
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static readonly string GoogleApiKey = config["GoogleApiKey"];
    }
}

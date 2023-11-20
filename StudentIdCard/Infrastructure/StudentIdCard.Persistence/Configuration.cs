
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence
{
    public static class Configuration
    {
        public static string GetConnectionString()
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/StudentIdCard.Presentation"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("ConnectionStrings");
        }
    }
}

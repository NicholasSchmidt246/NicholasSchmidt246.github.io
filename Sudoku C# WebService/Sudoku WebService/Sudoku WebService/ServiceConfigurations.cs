using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace Sudoku_WebService
{
    public static class ServiceConfigurations
    {
        /// <summary>
        /// Get environment variable named appSettingId.
        /// Should no environment variable exist named appSettingId, return value from config file named appSettingId.
        /// </summary>
        /// <param name="appSettingId"></param>
        /// <returns></returns>
        public static string GetAppSetting(string appSettingId, IConfiguration configuration)
        {
            string AppSetting = Environment.GetEnvironmentVariable(appSettingId);
            
            if (AppSetting == null)
            {
                AppSetting = configuration.GetSection(appSettingId).Value;
            }

            return AppSetting;
        }
        /// <summary>
        /// Get environment variable named connectionStringId.
        /// Should no environment variable exist named connectionStringId, return value from config file named connectionStringId.
        /// <param name="connectionStringId"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionStringId, IConfiguration configuration)
        {
            string ConnectionString = Environment.GetEnvironmentVariable(connectionStringId);
            
            if (ConnectionString == null)
            {
                ConnectionString = configuration.GetConnectionString(connectionStringId);
            }

            return ConnectionString;
        }
    }
}

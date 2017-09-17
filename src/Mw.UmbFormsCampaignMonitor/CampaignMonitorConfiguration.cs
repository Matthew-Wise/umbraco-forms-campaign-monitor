using System.Configuration;

namespace Mw.UmbFormsCampaignMonitor
{
    internal static class CampaignMonitorConfiguration
    {        
        public static string ApiKey { get { return GetConfigSetting("umbFormCampApiKey"); } }

        public static string ClientId { get { return GetConfigSetting("umbFormCampClientId"); ; } }

        private static string GetConfigSetting(string settingName)
        {
            var setting = ConfigurationManager.AppSettings[settingName];
            return setting ?? string.Empty;
        }       
    }
}

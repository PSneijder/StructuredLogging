using System;
using System.Configuration;
using System.Linq;
using System.Web.Hosting;

namespace StructuredLogging.Core
{
    abstract class SettingsBase
    {
        public TReturnType GetSetting<TReturnType>(string name, TReturnType defaultValue)
        {
            string configFileName = HostingEnvironment.IsHosted ? "web.config" : "app.config";
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };

            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            AppSettingsSection settings = configuration.AppSettings;

            if (!settings.Settings.AllKeys.Contains(name))
            {
                return defaultValue;
            }

            KeyValueConfigurationElement setting = settings.Settings[name];
            TReturnType value;

            try
            {
                value = (TReturnType) Convert.ChangeType(setting.Value, typeof(TReturnType));
            }
            catch (Exception)
            {
                return defaultValue;
            }

            return value;
        }
    }
}
using System;
using System.Configuration;
using System.Linq;
using System.Web.Hosting;

namespace StructuredLogging.Core
{
    abstract class SettingsBase
    {
        public T GetSetting<T>(string name, T defaultValue)
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
            T value;

            try
            {
                value = (T)Convert.ChangeType(setting.Value, typeof(T));
            }
            catch (Exception)
            {
                throw;
            }

            return value;
        }
    }
}
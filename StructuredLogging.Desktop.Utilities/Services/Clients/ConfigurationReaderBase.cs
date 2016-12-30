using System.Configuration;

namespace StructuredLogging.Desktop.Utilities.Services.Clients
{
    public abstract class ConfigurationReaderBase
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly Configuration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationReaderBase"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        protected ConfigurationReaderBase(Configuration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Reads the setting specified by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Setting not found:  + name</exception>
        protected string ReadSetting(string name)
        {
            KeyValueConfigurationElement settings = _configuration.AppSettings.Settings[name];

            if (settings == null)
                throw new ConfigurationErrorsException("Setting not found: " + name);

            return settings.Value;
        }
    }
}
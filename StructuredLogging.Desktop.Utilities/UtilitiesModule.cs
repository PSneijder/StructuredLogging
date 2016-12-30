using System.Configuration;
using Ninject.Modules;
using StructuredLogging.Desktop.Utilities.Services;
using StructuredLogging.Desktop.Utilities.Services.Clients;

namespace StructuredLogging.Desktop.Utilities
{
    public sealed class UtilitiesModule
        : NinjectModule
    {
        public override void Load()
        {
            Bind<IServiceClient>()
                .To<ServiceClient>()
                    .WithConstructorArgument(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));

            Bind<IDialogService>()
                .To<DialogService>()
                    .InSingletonScope();

            Bind<IShellService>()
                .To<ShellService>()
                    .InSingletonScope();
        }
    }
}
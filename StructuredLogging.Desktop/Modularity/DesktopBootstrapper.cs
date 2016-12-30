using System;
using System.Windows;
using Ninject;
using Prism.Modularity;
using Prism.Ninject;
using StructuredLogging.Desktop.Extensions;
using StructuredLogging.Desktop.Views;

namespace StructuredLogging.Desktop.Modularity
{
    internal sealed class DesktopBootstrapper
        : NinjectBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Kernel.Get<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }
        
        protected override void ConfigureKernel()
        {
            base.ConfigureKernel();

            Kernel.Initialize();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            Type toolItemModule = typeof(EventsModule.EventsModule);
            ModuleCatalog.AddModule(new ModuleInfo(toolItemModule.Name, toolItemModule.AssemblyQualifiedName));
        }
    }
}
using System.Windows;
using StructuredLogging.Desktop.Modularity;

namespace StructuredLogging.Desktop
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new DesktopBootstrapper();
            bootstrapper.Run();
        }
    }
}
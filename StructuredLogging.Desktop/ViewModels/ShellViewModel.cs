using Prism.Regions;
using StructuredLogging.Desktop.EventsModule.Views;
using StructuredLogging.Desktop.Utilities.Modularity;
using StructuredLogging.Desktop.Utilities.ViewModels;

namespace StructuredLogging.Desktop.ViewModels
{
    public sealed class ShellViewModel
        : ViewModelBase
    {
        public ShellViewModel(IRegionManager regionManager)
        {
            regionManager.RequestNavigate(RegionNames.MainRegion, typeof(EventsView).FullName);
        }
    }
}
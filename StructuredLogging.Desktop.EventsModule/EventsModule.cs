using System;
using System.Windows;
using Prism.Modularity;
using Prism.Regions;
using StructuredLogging.Desktop.EventsModule.Views;
using StructuredLogging.Desktop.Utilities.Modularity;

namespace StructuredLogging.Desktop.EventsModule
{
    public sealed class EventsModule
        : IModule
    {
        private readonly IRegionManager _regionManager;

        public EventsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri($"pack://application:,,,/{typeof(EventsModule).Namespace};component/Resources/EventsDictionary.xaml")
            });

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(EventsView));
        }
    }
}
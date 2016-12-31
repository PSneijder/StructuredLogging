using System;
using MahApps.Metro.Controls;
using StructuredLogging.Desktop.EventsModule.ViewModels;

namespace StructuredLogging.Desktop.EventsModule.Views
{
    public partial class EventsView
    {
        public EventsView(EventsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
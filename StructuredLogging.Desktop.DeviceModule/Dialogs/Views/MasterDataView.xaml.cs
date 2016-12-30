
using StructuredLogging.Desktop.EventsModule.Dialogs.ViewModels;

namespace StructuredLogging.Desktop.EventsModule.Dialogs.Views
{
    public partial class MasterDataView
    {
        public MasterDataView(MasterDataViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
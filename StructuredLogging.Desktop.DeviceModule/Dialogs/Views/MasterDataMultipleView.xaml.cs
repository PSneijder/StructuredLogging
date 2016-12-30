using StructuredLogging.Desktop.EventsModule.Dialogs.ViewModels;

namespace StructuredLogging.Desktop.EventsModule.Dialogs.Views
{
    public partial class MasterDataMultipleView
    {
        public MasterDataMultipleView(MasterDataMultipleViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
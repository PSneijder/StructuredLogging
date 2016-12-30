using StructuredLogging.Desktop.ViewModels;

namespace StructuredLogging.Desktop.Views
{
    public partial class Shell
    {
        public Shell(ShellViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
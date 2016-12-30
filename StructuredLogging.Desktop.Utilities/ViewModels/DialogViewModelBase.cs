using System.Collections.Generic;
using StructuredLogging.Desktop.Utilities.Models;
using StructuredLogging.Desktop.Utilities.Services;

namespace StructuredLogging.Desktop.Utilities.ViewModels
{
    public abstract class DialogViewModelBase
        : ViewModelBase
        , IHaveHaveDialogView
    {
        private bool _hasErrors;
        private string _validationSummary;

        public bool HasErrors
        {
            get { return _hasErrors; }
            set { SetPropertyChanged(ref _hasErrors, value); }
        }

        public string ValidationSummary
        {
            get { return _validationSummary; }
            set { SetPropertyChanged(ref _validationSummary, value); }
        }

        public abstract IEnumerable<DialogActionCommand> Actions { get; protected set; }
        public abstract string SubTitle { get; protected set; }
        public abstract void OnNavigatedTo(IDictionary<string, object> parameters);
    }
}
using System.Collections.Generic;
using StructuredLogging.Desktop.Utilities.Models;

namespace StructuredLogging.Desktop.Utilities.Services
{
    public interface IHaveHaveDialogView
    {
        IEnumerable<DialogActionCommand> Actions { get; }
        string SubTitle { get; }

        void OnNavigatedTo(IDictionary<string, object> parameters);
    }
}
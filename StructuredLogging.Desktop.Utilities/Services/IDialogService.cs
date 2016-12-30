using System.Collections.Generic;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace StructuredLogging.Desktop.Utilities.Services
{
    public delegate void DialogClosedEventHandler();

    public interface IDialogService
    {
        event DialogClosedEventHandler DialogClosed;

        Task ShowDialogAsync<TDialogView>(IDictionary<string, object> parameters = null) where TDialogView : CustomDialog;

        Task HideDialogAsync<TDialogView>() where TDialogView : CustomDialog;
    }
}
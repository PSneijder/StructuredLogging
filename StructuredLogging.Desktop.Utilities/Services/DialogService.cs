using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Ninject;

namespace StructuredLogging.Desktop.Utilities.Services
{
    public sealed class DialogService
        : IDialogService
    {
        private readonly IKernel _kernel;

        public event DialogClosedEventHandler DialogClosed;

        private void OnDialogClosed()
        {
            var handler = DialogClosed;
            if (handler != null)
                handler();
        }

        public DialogService(IKernel kernel)
        {
            _kernel = kernel;
        }

        public Task ShowDialogAsync<TDialogView>(IDictionary<string, object> parameters = null) where TDialogView : CustomDialog
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            if (metroWindow != null)
            {
                var settings = new MetroDialogSettings
                {
                    AnimateHide = true,
                    AnimateShow = true,
                    ColorScheme = MetroDialogColorScheme.Inverted
                };

                var view = _kernel.Get<TDialogView>();

                var context = view.DataContext as IHaveHaveDialogView;
                if (context != null)
                {
                    context.OnNavigatedTo(parameters);
                }
                
                return metroWindow.ShowMetroDialogAsync(view, settings);
            }

            return null;
        }

        public async Task HideDialogAsync<TDialogView>() where TDialogView : CustomDialog
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            if (metroWindow != null)
            {
                var view = await metroWindow.GetCurrentDialogAsync<TDialogView>();

                OnDialogClosed();

                await metroWindow.HideMetroDialogAsync(view);
            }
        }
    }
}
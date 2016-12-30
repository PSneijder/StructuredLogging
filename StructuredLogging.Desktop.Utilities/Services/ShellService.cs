using System.Windows;
using MahApps.Metro.Controls;

namespace StructuredLogging.Desktop.Utilities.Services
{
    public sealed class ShellService
        : IShellService
    {
        public void ChangeWindowSize(int height, int width)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            if (window != null)
            {
                window.Height = height;
                window.Width = width;
            }
        }
    }
}

using System.Windows.Input;

namespace StructuredLogging.Desktop.Utilities.Models
{
    public sealed class DialogActionCommand
    {
        public ICommand Command { get; set; }
        public object Parameter { get; set; }
        public bool IsEnabled { get; set; }
        public object Content { get; set; }
        public bool IsDefault { get; set; }
        public bool IsVisible { get; set; }
    }
}
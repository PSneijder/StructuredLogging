using System.Windows;
using System.Windows.Controls;

namespace StructuredLogging.Desktop.Utilities.Selectors
{
    public sealed class QueryFilterSelector
        : DataTemplateSelector
    {
        public DataTemplate DefaultFilterDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return DefaultFilterDataTemplate;
        }
    }
}
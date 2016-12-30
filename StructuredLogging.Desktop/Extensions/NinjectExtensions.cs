using Ninject;
using StructuredLogging.Desktop.Utilities;

namespace StructuredLogging.Desktop.Extensions
{
    public static class NinjectExtensions
    {
        public static void Initialize(this IKernel kernel)
        {
            kernel.Load(new UtilitiesModule(), new StructuredLoggingModule());
        }
    }
}
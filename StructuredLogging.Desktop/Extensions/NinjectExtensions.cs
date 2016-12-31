using Ninject;
using StructuredLogging.Desktop.Utilities;

namespace StructuredLogging.Desktop.Extensions
{
    static class NinjectExtensions
    {
        public static void Initialize(this IKernel kernel)
        {
            kernel.Load(new UtilitiesModule(), new StructuredLoggingModule());
        }
    }
}
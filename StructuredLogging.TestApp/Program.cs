using System;
using Ninject;

namespace StructuredLogging.TestApp
{
    class Program
    {
        static void Main()
        {
            using (var kernel = new StandardKernel(new TestAppModule(), new StructuredLoggingModule()))
            {
                var runner = kernel.Get<Runner>();
                runner.Run();
            }

            Console.WriteLine("Press ANY key to continue.");
            Console.ReadKey();
        }
    }
}
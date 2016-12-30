using System;
using Ninject;

namespace StructuredLogging.TestApp
{
    class Program
    {
        static void Main()
        {
            using (var kernel = new StandardKernel())
            {
                var runner = new Runner(kernel);
                runner.Run();
            }

            Console.WriteLine("Press ANY key to continue.");
            Console.ReadKey();
        }
    }
}
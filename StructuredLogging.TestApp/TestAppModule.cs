using Ninject.Modules;

namespace StructuredLogging.TestApp
{
    public sealed class TestAppModule
        : NinjectModule
    {
        public override void Load()
        {
            Bind<Runner>().ToSelf();
        }
    }
}
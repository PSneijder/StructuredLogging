using Ninject.Modules;
using System;
using System.IO;
using StructuredLogging.Core;
using StructuredLogging.Core.Contracts;
using StructuredLogging.Services;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging
{
    public sealed class StructuredLoggingModule
        : NinjectModule
    {
        public override void Load()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage");

            Bind<IIndexer>()
                .To<Indexer>()
                    .InSingletonScope()
                        .WithConstructorArgument("path", path)
                            .WithConstructorArgument("recreateIfExists", true);
            Bind<ISearcher>()
                .To<Searcher>()
                    .InSingletonScope()
                        .WithConstructorArgument("path", path);

            Bind<IFilterBuilder>()
                .To<FilterBuilder>()
                    .InSingletonScope();
            Bind<IFormater>()
                .To<Formater>()
                    .InSingletonScope();

            Bind<IIndexerService>()
                .To<IndexerService>()
                    .InSingletonScope();
            Bind<ISearcherService>()
                .To<SearcherService>()
                    .InSingletonScope();
            Bind<IQueryService>()
                .To<QueryService>()
                    .InSingletonScope();
        }
    }
}
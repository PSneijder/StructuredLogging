using System;
using System.Collections.Generic;
using Ninject;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.DataContracts.Query;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.TestApp
{
    internal sealed class Runner
    {
        private readonly IKernel _kernel;

        public Runner(IKernel kernel)
        {
            _kernel = kernel;
            _kernel.Load(new StructuredLoggingModule());
        }

        private RawEvents CreateSampleEvents()
        {
            var warningRawEvent = new RawEvent
            {
                Timestamp = new DateTime(2016, 1, 1, 6, 4, 2).ToString("O"),
                Level = "Warning",
                MessageTemplate = "Disk space is low on {Drive} on {MachineName}",
                Properties = new Dictionary<string, object>
                {
                    { "Drive", "C:" },
                    { "MachineName", "nblumhardt-rmbp" }
                }
            };

            var errorRawEvent = new RawEvent
            {
                Timestamp = new DateTime(2016, 1, 10, 19, 7, 1).ToString("O"),
                Level = "Error",
                MessageTemplate = "Disk space is full on {Drive} on {MachineName}",
                Properties = new Dictionary<string, object>
                {
                    { "Drive", "D:" },
                    { "MachineName", "nbherb-rmbp" }
                }
            };

            var rawEvents = new RawEvents
            {
                Events = new[]
                {
                    warningRawEvent,
                    errorRawEvent,
                    new RawEvent
                    {
                        Timestamp = new DateTime(2016, 2, 12, 12, 4, 9).ToString("O"),
                        Level = "Error",
                        MessageTemplate = "Disk space is full on {Drive} on {MachineName}",
                        Properties = new Dictionary<string, object>
                        {
                            { "Drive", "E:" },
                            { "MachineName", "nbstulz-rmbp" }
                        }
                    }
                }
            };

            return rawEvents;
        }

        public void Run()
        {
            var rawEvents = CreateSampleEvents();
            
            var indexer = _kernel.Get<IIndexerService>();
            indexer.Index(rawEvents);
            
            var searcher = _kernel.Get<ISearcherService>();
            var request = new SearchRequest("disk", new[]
                {
                    new QueryFilterProperty(DataContracts.Constants.Facet.Level, "Warning"), 
                    //new QueryFilterProperty(DataContracts.Constants.Facet.Level, "Error")
                });

            var results = searcher.Search(request);
            foreach (var result in results.Results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
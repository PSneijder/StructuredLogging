using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Newtonsoft.Json;
using System;
using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.Extensions;

namespace StructuredLogging.Core
{
    sealed class Indexer
        : SettingsBase
            , IIndexer
    {
        private IndexWriter _writer;

        public Indexer(string path, bool recreateIfExists = false)
            : this(FSDirectory.Open(path), Fields.CreatePerFieldAnalyzers(), recreateIfExists) { }

        public Indexer(FSDirectory indexDirectory, Analyzer analyzer, bool recreateIfExists)
        {
            Initialize(indexDirectory, analyzer, recreateIfExists);
        }

        private void Initialize(FSDirectory indexDirectory, Analyzer analyzer, bool recreateIfExists)
        {
            _writer = new IndexWriter(indexDirectory, analyzer, recreateIfExists, new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH));

            LoadAndSetConfiguration(_writer);
        }

        private void LoadAndSetConfiguration(IndexWriter writer)
        {
            int size = GetSetting("RAMBufferSizeMB", 1024);
            int mergeFactor = GetSetting("MergeFactor", 40);
            int termIndexInterval = GetSetting("TermIndexInterval", 1024);
            bool useCompoundFile = GetSetting("UseCompoundFile", false);

            writer.SetRAMBufferSizeMB(size);
            writer.MergeFactor = mergeFactor;
            writer.SetMergePolicy(new LogDocMergePolicy(writer));
            writer.SetMergeScheduler(new ConcurrentMergeScheduler());
            writer.TermIndexInterval = termIndexInterval;
            writer.UseCompoundFile = useCompoundFile;
        }

        public void Index(RawEvents rawEvents)
        {
            Index(rawEvents.Events);
        }

        public void Index(params RawEvent[] rawEvents)
        {
            foreach (var rawEvent in rawEvents)
            {
                var properties = JsonConvert.SerializeObject(rawEvent.Properties);
                var timeStamp = DateTime.Parse(rawEvent.Timestamp).ToUnixTimeStamp();

                var document = new Document();
                document.AddNumericField(Fields.Timestamp, timeStamp, Field.Index.NOT_ANALYZED);
                document.AddField(DataContracts.Constants.Facet.Level, rawEvent.Level, Field.Index.NOT_ANALYZED);
                document.AddField(Fields.MessageTemplate, rawEvent.MessageTemplate, Field.Index.ANALYZED);
                document.AddField(Fields.Properties, properties, Field.Index.ANALYZED);

                foreach (var property in rawEvent.Properties)
                {
                    document.AddField(DataContracts.Constants.Facet.PropertyField, property.Key, Field.Index.NOT_ANALYZED);
                }

                _writer.AddDocument(document);
                _writer.Commit();
            }

            _writer.Optimize();
        }

        public void Dispose()
        {
            try
            {
                _writer?.Commit();
                _writer?.Dispose();
            }
            catch (Exception)
            {
                /* TODO */
            }
        }
    }
}
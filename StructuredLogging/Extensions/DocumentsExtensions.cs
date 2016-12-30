using Lucene.Net.Documents;
using System;
using BoboBrowse.Net;
using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.Extensions
{
    static class DocumentsExtensions
    {
        public static SearchResultItem ToSearchResultItem(this Document document, IFormater formater, BrowseResult result)
        {
            var rawEvent = document.ToRawEvent();

            var message = formater.FormatEvent(rawEvent);
            var timeStamp = DateTime.Parse(rawEvent.Timestamp);
            
            return new SearchResultItem(rawEvent.Level, timeStamp, message);
        }

        public static RawEvent ToRawEvent(this Document document)
        {
            var level = document.GetField(DataContracts.Constants.Facet.Level).AsString();
            var timestamp = document.GetField(Fields.Timestamp).AsDouble().FromUnixTimeStamp();
            var properties = document.GetField(Fields.Properties).ToDictionary();
            var messageTemplate = document.GetField(Fields.MessageTemplate).AsString();

            return new RawEvent
            {
                Level = level,
                Timestamp = timestamp.ToString("O"),
                MessageTemplate = messageTemplate,
                Properties = properties
            };
        }

        public static Document AddField(this Document document, string name, string value, Field.Index index, Field.Store store = Field.Store.YES)
        {
            document.Add(new Field(name, value ?? string.Empty, store, index));
            return document;
        }

        public static Document AddField(this Document document, string name, string value, Field.Index index, float boost, Field.Store store = Field.Store.YES)
        {
            document.Add(new Field(name, value ?? string.Empty, store, index) { Boost = boost });
            return document;
        }

        public static Document AddNumericField(this Document document, string name, double value, Field.Index index, Field.Store store = Field.Store.YES)
        {
            var numericField = new NumericField(name, store, true);
            numericField.SetDoubleValue(Convert.ToDouble(value));

            document.Add(numericField);
            return document;
        }
    }
}
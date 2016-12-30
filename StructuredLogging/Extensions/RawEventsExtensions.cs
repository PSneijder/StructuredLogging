using System;
using System.Collections.Generic;
using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.Extensions
{
    internal static class RawEventsExtensions
    {
        public static IEnumerable<SearchResultItem> ToSearchResultItems(this IEnumerable<RawEvent> rawEvents, IFormater formater)
        {
            foreach (var rawEvent in rawEvents)
            {
                var message = formater.FormatEvent(rawEvent);
                var timeStamp = DateTime.Parse(rawEvent.Timestamp);

                yield return new SearchResultItem(rawEvent.Level, timeStamp, message);
            }
        }
    }
}
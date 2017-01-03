using System;
using System.Collections.Generic;
using System.Linq;
using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.Extensions
{
    static class RawEventExtensions
    {
        public static SearchResultItem ToSearchResultItem(this RawEvent rawEvent, IFormater formater)
        {
            var message = formater.FormatEvent(rawEvent);
            var timeStamp = DateTime.Parse(rawEvent.Timestamp);

            return new SearchResultItem(rawEvent.Level, timeStamp, message);
        }

        public static IEnumerable<SearchResultItem> ToSearchResultItems(this IEnumerable<RawEvent> rawEvents, IFormater formater)
        {
            return rawEvents.Select(rawEvent => rawEvent.ToSearchResultItem(formater));
        }
    }
}
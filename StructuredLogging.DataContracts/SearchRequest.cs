using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.DataContracts
{
    [DebuggerDisplay("{Phrase} {StartDate} {EndDate}")]
    public struct SearchRequest
    {
        public string Phrase { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }

        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public QueryFilterItem[] Items { get; private set; }

        public SearchRequest(string phrase, IEnumerable<QueryFilterItem> properties = null, DateTime? startDate = null, DateTime? endDate = null, int skip = 0, int take = 0)
        {
            Phrase = phrase;
            StartDate = startDate;
            EndDate = endDate;
            Skip = skip;
            Take = take;
            Items = properties?.ToArray() ?? new QueryFilterItem[0];
        }

        public override string ToString()
        {
            return $"{Phrase} {StartDate:G} {EndDate:G}";
        }
    }
}
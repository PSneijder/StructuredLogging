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
        public string Phrase { get; }
        public int Skip { get; }
        public int Take { get; }

        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public QueryFilterProperty[] Properties { get; }

        public SearchRequest(string phrase, IEnumerable<QueryFilterProperty> properties = null, DateTime? startDate = null, DateTime? endDate = null, int skip = 0, int take = 0)
        {
            Phrase = phrase;
            StartDate = startDate;
            EndDate = endDate;
            Skip = skip;
            Take = take;
            Properties = properties?.ToArray() ?? new QueryFilterProperty[0];
        }

        public override string ToString()
        {
            return $"{Phrase} {StartDate:G} {EndDate:G}";
        }
    }
}
using BoboBrowse.Net;
using Lucene.Net.Search;
using System.Collections.Generic;
using StructuredLogging.DataContracts.Query;
using Query = StructuredLogging.DataContracts.Query.Query;

namespace StructuredLogging.Extensions
{
    static class BrowseRequestExtensions
    {
        public static void AddQueryFilter(this BrowseRequest request, Query query)
        {
            foreach (var selection in CreateSelection(query))
            {
                request.AddSelection(selection.Value);
            }

            request.Query = new BooleanQuery
            {
                { request.Query, Occur.MUST },
                { NumericRangeQuery.NewDoubleRange(Fields.Timestamp, query.From.ToUnixTimeStamp(), query.To.ToUnixTimeStamp(), true, true), Occur.MUST }
            };
        }

        public static void AddQueryProperty(this BrowseRequest request, QueryFilterItem item)
        {
            var selection = new BrowseSelection(item.Name)
            {
                SelectionOperation = BrowseSelection.ValueOperation.ValueOperationOr,
            };

            request.AddSelection(selection);
        }

        private static IDictionary<string, BrowseSelection> CreateSelection(Query query)
        {
            var selectionMap = new Dictionary<string, BrowseSelection>();
            foreach (var property in query.Items)
            {
                BrowseSelection selection;
                if (selectionMap.TryGetValue(property.Name, out selection))
                {
                    selection.AddValue(property.Value);
                }
                else
                {
                    selection = new BrowseSelection(property.Name)
                    {
                        Values = new[] { property.Value }
                    };

                    selectionMap.Add(property.Name, selection);
                }
            }

            return selectionMap;
        }
    }
}
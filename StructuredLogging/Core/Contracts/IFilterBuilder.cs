using System.Collections.Generic;
using BoboBrowse.Net;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.Core.Contracts
{
    interface IFilterBuilder
    {
        IEnumerable<QueryFilterGroup> Create(BrowseResult result);
    }
}
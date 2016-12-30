using System.Collections.Generic;
using System.Threading;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Util;

namespace StructuredLogging.Core.QueryParsers
{
    sealed class MultiFieldWildcardQueryParser
        : MultiFieldQueryParser
    {
        public MultiFieldWildcardQueryParser(Version matchVersion, string[] fields, Analyzer analyzer, IDictionary<string, float> boosts)
            : base(matchVersion, fields, analyzer, boosts) { Initialize(); }

        public MultiFieldWildcardQueryParser(Version matchVersion, string[] fields, Analyzer analyzer)
            : base(matchVersion, fields, analyzer) { Initialize(); }

        public override Query Parse(string searchTerms)
        {
            var query = (BooleanQuery) base.Parse(searchTerms);

            foreach (var clause in query.Clauses)
            {
                var term = ((TermQuery) clause.Query).Term;
                var booleanQuery = new BooleanQuery
                    {
                        { new WildcardQuery(new Term(term.Field, $"*{term.Text}*")), Occur.SHOULD },
                        { new WildcardQuery(new Term(term.Field, $"*{term.Text}")), Occur.SHOULD },
                        { new WildcardQuery(new Term(term.Field, $"{term.Text}*")), Occur.SHOULD },
                        { new WildcardQuery(new Term(term.Field, $"{term.Text}")), Occur.SHOULD }
                    };

                clause.Query = booleanQuery;
            }

            return query;
        }

        private void Initialize()
        {
            DefaultOperator = QueryParser.Operator.AND;
            Locale = Thread.CurrentThread.CurrentCulture;
            AllowLeadingWildcard = true;
        }
    }
}
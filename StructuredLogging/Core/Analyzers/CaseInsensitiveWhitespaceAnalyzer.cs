using System.IO;
using Lucene.Net.Analysis;

namespace StructuredLogging.Core.Analyzers
{
    sealed class CaseInsensitiveWhitespaceAnalyzer
        : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            return new LowerCaseFilter(new WhitespaceTokenizer(reader));
        }
    }
}
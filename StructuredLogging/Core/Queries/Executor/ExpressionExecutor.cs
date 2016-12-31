using System;
using Superpower;
using StructuredLogging.Core.Queries.Parser;
using StructuredLogging.Core.Queries.Tokenization;

namespace StructuredLogging.Core.Queries.Executor
{
    static class ExpressionExecutor
    {
        public static int? Execute(string expression)
        {
            try
            {
                var tok = new ExpressionTokenizer();
                var tokens = tok.Tokenize(expression);

                var expr = ExpressionParser.Lambda.Parse(tokens);
                var compiled = expr.Compile();

                var result = compiled();

                return result;
            }
            catch (ParseException)
            {
                /* TODO */
            }
            catch (Exception)
            {
                /* TODO */
            }

            return null;
        }
    }
}
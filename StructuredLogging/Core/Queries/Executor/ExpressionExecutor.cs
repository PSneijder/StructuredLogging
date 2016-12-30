
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
                Console.WriteLine(result);

                return result;
            }
            catch (ParseException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }

            return null;
        }
    }
}
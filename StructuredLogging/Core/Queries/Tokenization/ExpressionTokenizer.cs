using Superpower;
using Superpower.Model;
using System.Collections.Generic;
using Superpower.Parsers;

namespace StructuredLogging.Core.Queries.Tokenization
{
    sealed class ExpressionTokenizer
        : Tokenizer<ExpressionToken>
    {
        readonly Dictionary<char, ExpressionToken> _operators = new Dictionary<char, ExpressionToken>
        {
            ['+'] = ExpressionToken.Plus,
            ['-'] = ExpressionToken.Minus,
            ['*'] = ExpressionToken.Times,
            ['/'] = ExpressionToken.Divide,
            ['('] = ExpressionToken.LParen,
            [')'] = ExpressionToken.RParen,
        };

        protected override IEnumerable<Result<ExpressionToken>> Tokenize(TextSpan span)
        {
            var next = SkipWhiteSpace(span);

            if (!next.HasValue)
            { 
                yield break;
            }

            do
            {
                ExpressionToken charToken;

                if (char.IsDigit(next.Value))
                {
                    var integer = Numerics.Integer(next.Location);
                    next = integer.Remainder.ConsumeChar();
                    yield return Result.Value(ExpressionToken.Number, integer.Location, integer.Remainder);
                }
                else if (_operators.TryGetValue(next.Value, out charToken))
                {
                    yield return Result.Value(charToken, next.Location, next.Remainder);
                    next = next.Remainder.ConsumeChar();
                }
                else
                {
                    yield return Result.Empty<ExpressionToken>(next.Location, new[] { "number", "operator" });
                }

                next = SkipWhiteSpace(next.Location);

            } while (next.HasValue);
        }
    }
}
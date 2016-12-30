using System;
using System.Linq.Expressions;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using StructuredLogging.Core.Queries.Tokenization;

namespace StructuredLogging.Core.Queries.Parser
{
    sealed class ExpressionParser
    {
        private static TokenListParser<ExpressionToken, ExpressionType> Operator(ExpressionToken op, ExpressionType opType)
        {
            return Token.EqualTo(op).Value(opType);
        }

        private static readonly TokenListParser<ExpressionToken, ExpressionType> Add = Operator(ExpressionToken.Plus, ExpressionType.AddChecked);
        private static readonly TokenListParser<ExpressionToken, ExpressionType> Subtract = Operator(ExpressionToken.Minus, ExpressionType.SubtractChecked);
        private static readonly TokenListParser<ExpressionToken, ExpressionType> Multiply = Operator(ExpressionToken.Times, ExpressionType.MultiplyChecked);
        private static readonly TokenListParser<ExpressionToken, ExpressionType> Divide = Operator(ExpressionToken.Divide, ExpressionType.Divide);

        private static readonly TokenListParser<ExpressionToken, Expression> Constant = Token.EqualTo(ExpressionToken.Number).Apply(Numerics.IntegerInt32).Select(n => (Expression)Expression.Constant(n));
        private static readonly TokenListParser<ExpressionToken, Expression> Factor = (from lparen in Token.EqualTo(ExpressionToken.LParen) from expr in Parse.Ref(() => Expr) from rparen in Token.EqualTo(ExpressionToken.RParen) select expr).Or(Constant);

        private static readonly TokenListParser<ExpressionToken, Expression> Operand = (from sign in Token.EqualTo(ExpressionToken.Minus) from factor in Factor select (Expression)Expression.Negate(factor)).Or(Factor).Named("expression");
        private static readonly TokenListParser<ExpressionToken, Expression> Term = Parse.Chain(Multiply.Or(Divide), Operand, Expression.MakeBinary);
        private static readonly TokenListParser<ExpressionToken, Expression> Expr = Parse.Chain(Add.Or(Subtract), Term, Expression.MakeBinary);

        public static readonly TokenListParser<ExpressionToken, Expression<Func<int>>> Lambda = Expr.AtEnd().Select(body => Expression.Lambda<Func<int>>(body));

        public static bool IsValid(string expression)
        {
            Result<TokenList<ExpressionToken>> result = new ExpressionTokenizer().TryTokenize(expression);

            return result.HasValue && Expr.TryParse(result.Value).HasValue;
        }
    }
}
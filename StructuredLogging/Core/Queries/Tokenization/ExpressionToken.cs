using Superpower.Display;

namespace StructuredLogging.Core.Queries.Tokenization
{
    enum ExpressionToken
    {
        None,
        Number,
        [Token(Category = "operator", Example = "+")]
        Plus,
        [Token(Category = "operator", Example = "-")]
        Minus,
        [Token(Category = "operator", Example = "*")]
        Times,
        [Token(Category = "operator", Example = "-")]
        Divide,
        [Token(Example = "(")]
        LParen,
        [Token(Example = ")")]
        RParen
    }
}
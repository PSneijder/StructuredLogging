using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructuredLogging.Core.Queries.Executor;
using StructuredLogging.Core.Queries.Parser;

namespace StructuredLogging.Tests
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void TestParserWithValidExpression()
        {
            // Given
            var expression = "(1 + 2) * 3";

            // When
            var isValid = ExpressionParser.IsValid(expression);

            // Then
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void TestParserWithInValidExpression()
        {
            // Given
            var expression = "(1 + 2) * a";

            // When
            var isValid = ExpressionParser.IsValid(expression);

            // Then
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void TestExecutorWithValidExpression()
        {
            // Given
            var expression = "(1 + 2) * 3";

            // When
            var returnValue = ExpressionExecutor.Execute(expression);

            // Then
            Assert.AreEqual(returnValue, 9);
        }

        [TestMethod]
        public void TestExecutorWithInValidExpression()
        {
            // Given
            var expression = "(1 + 2) * a";

            // When
            var returnValue = ExpressionExecutor.Execute(expression);

            // Then
            Assert.AreEqual(returnValue, null);
        }
    }
}
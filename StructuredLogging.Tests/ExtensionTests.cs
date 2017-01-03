using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.Extensions;

namespace StructuredLogging.Tests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void TestFormatWith()
        {
            // Given
            var item = new RawEvent
            {
                Level = "Warning",
                Properties = new Dictionary<string, object>
                {
                    { "Count", 124 },
                    { "Message", "Hello World" }
                }
            };

            // When
            var expected = "{Count}".FormatWith(item.Properties);

            // Then
            Assert.IsNotNull(expected);
        }
    }
}
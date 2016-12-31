using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructuredLogging.Core;
using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.Tests
{
    [TestClass]
    public class MessageTemplateParserTests
    {
        [TestMethod]
        public void TestSimpleParser()
        {
            var rawEvent = new RawEvent
            {
                Level = "Warning",
                MessageTemplate = "What is going on {Count} with {Message}",
                Properties = new Dictionary<string, object>
                {
                    { "Count", 124 },
                    { "Message", "Hello World" }
                }
            };

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                {
                    string formatted = new Formater().FormatEvent(rawEvent);
                }
            }
        }
    }
}
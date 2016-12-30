using Serilog.Events;
using Serilog.Parsing;
using System.Collections.Generic;
using System.Linq;
using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.Core
{
    sealed class Formater
        : IFormater
    {
        private readonly MessageTemplateParser _templateParser = new MessageTemplateParser();

        public string FormatEvent(RawEvent logEvent)
        {
            MessageTemplate messageTemplate = _templateParser.Parse(logEvent.MessageTemplate);
            Dictionary<string, LogEventPropertyValue> properties = new Dictionary<string, LogEventPropertyValue>();

            foreach (PropertyToken propertyToken in messageTemplate.Tokens.OfType<PropertyToken>())
            {
                object value;
                if (logEvent.Properties.TryGetValue(propertyToken.PropertyName, out value))
                {
                    properties.Add(propertyToken.PropertyName, new ScalarValue(value));
                }
            }

            return messageTemplate.Render(properties);
        }
    }
}
# StructuredLogging

StructuredLogging collects data over HTTP, while your applications use the best available structured logging APIs for your platform. StructuredLogging is a web application hosted on your infrastructure, so you retain complete control over your own data.

StructuredLogging is a log server that runs on a central machine. Your applications internally write structured events with a framework like Serilog, NLog, log4net.

Structured logging preserves the individual property values, as well as the text, of each event.
These are sent across the network to Seq, which displays and makes them searchable:

![alt tag](https://raw.githubusercontent.com/PSneijder/StructuredLogging/master/Assets/StructuredLogging.png)

# QuickStart
TODO

# TODOs
* WPF Client.
* Angular2 Client.
* Refactoring (:

# Recent Changes
See [CHANGES.txt](CHANGES.txt)

# Committers
* [Philip Schneider](https://github.com/PSneijder)

# Licensing
The license for the code is [ALv2](http://www.apache.org/licenses/LICENSE-2.0.html).

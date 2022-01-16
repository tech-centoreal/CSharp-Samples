// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

using Microsoft.Extensions.Configuration;

Console.WriteLine("Welcome to Instrumentation Sample Code!");


Console.WriteLine("Loading configuration file");

// Configure loading appsettings file
ConfigurationBuilder builder = new();
builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfigurationRoot configuration = builder.Build();
TraceSwitch ts = new(
    displayName: "PacktSwitch",
    description: "This switch is set via a JSON config."
);
configuration.GetSection("PacktSwitch").Bind(ts);

Console.WriteLine("Configuring trace file path and listener");

string tracePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "trace.txt");

// Default folder path is user's root directory.
// You can provide full file path for the trace file
Trace.Listeners.Add(new TextWriterTraceListener(tracePath));

// Text writer is buffered, so this option calls Flush() on all listeners after writing
// Calls the Flush method automatically after every write.
// Trace.AutoFlush = true;


Debug.WriteLine("Dubug says, I am watching !!");

Trace.WriteLine("Trace says, I am watching !!");

Trace.WriteLineIf(ts.TraceError, "Trace Error");
Trace.WriteLineIf(ts.TraceWarning, "Trace Warning");
Trace.WriteLineIf(ts.TraceInfo, "Trace Info");
Trace.WriteLineIf(ts.TraceVerbose, "Trace Verbose");



// Write the logs, which is still there buffer, before program ends
Trace.Flush();
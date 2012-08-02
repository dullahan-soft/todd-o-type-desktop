using System;
using System.Text;
using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

class Options : CommandLineOptionsBase
{
    [Option("p", "port", HelpText = "Sets the port the arduino is on.")]
    public string Port { get; set; }

    [Option("s", "secret", HelpText = "Sets the secrete key used to communicate with the playlist server.")]
    public string Secret { get; set; }
    
    void HandleParsingErrorsInHelp(HelpText help)
    {
        if (this.LastPostParsingState.Errors.Count > 0)
        {
            var errors = help.RenderParsingErrorsText(this, 2); // indent with two spaces
            if (!string.IsNullOrEmpty(errors))
            {
   
                help.AddPreOptionsLine(string.Concat(Environment.NewLine, "ERROR(S):"));
                help.AddPreOptionsLine(errors);
            }
        }
    }
}
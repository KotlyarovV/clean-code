using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Markdown
{
	class Program
	{
	    private static readonly string[] launchProperties = { "--in", "--out" };

		static void Main(string[] args)
		{
            var launchPropertiesValues = new Dictionary<string, string>();
		    string fileInn;
            
            for (var i = 0; i < args.Length; i++)
            {
                if (launchProperties.Contains(args[i]))
                    launchPropertiesValues[args[i]] = args[i + 1];
            }

		    try
		    {
                fileInn = File.ReadAllText(launchPropertiesValues["--in"]);
		    }
		    catch 
		    {
		        Console.WriteLine("No in file");
                return;
		    }

		    string mdLine;
		    try
		    {
                var md = new Md();
		        mdLine = md.RenderToHtml(fileInn);
		    }
		    catch
		    {
		        Console.WriteLine("There was an error in parsing.");
                return;
		    }

		    if (!launchPropertiesValues.ContainsKey("--out"))
		    {
		        Console.WriteLine("Out file was not specified!");
                return;
		    }
            
		    File.WriteAllText(launchPropertiesValues["--out"], mdLine);
        }
	}
}

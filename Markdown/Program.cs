using System;
using System.IO;
using Fclp;

namespace Markdown
{
	class Program
	{
		static void Main(string[] args)
		{
		    var parser = new FluentCommandLineParser();

		    var fileOutName = string.Empty;
		    var fileInName = string.Empty;

		    parser.Setup<string>("out")
		        .Callback(str => fileOutName = str)
                .Required();

		    parser.Setup<string>("in")
		        .Callback(str => fileInName = str)
                .Required();

		    var result = parser.Parse(args);

		    if (result.HasErrors)
		    {
		        Console.WriteLine("One of the parameters was missed!");
		    }

		    string fileIn;

            try
            {
                fileIn = File.ReadAllText(fileInName);
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
                mdLine = md.RenderToHtml(fileIn);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error in parsing.");
                Console.WriteLine(e.Message);
                return;
            }
            
            File.WriteAllText(fileOutName , mdLine);
		}
	}
}

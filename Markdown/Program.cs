using System;
using System.IO;
using Fclp;

namespace Markdown
{
    public class ApplicationArguments
    {
        public string FileOutName { get; set; }
        public string FileInName { get; set; }
    }

    class Program
    {
        static Md md = new Md();

        private const string HelpText = "Prinat name of input file with --in parameter and outputfile with --out parameter."; 

        static void PrepareParser(FluentCommandLineParser<ApplicationArguments> parser)
        {
            parser.Setup(arg => arg.FileInName)
                .As("in")
                .Required();

            parser.Setup(arg => arg.FileOutName)
                .As("out")
                .Required();

            parser.SetupHelp("?", "help")
                .Callback(() => Console.WriteLine(HelpText))
                .UseForEmptyArgs();
        }

        static string GetStringFromFile(string fileName)
        {
            string fileIn = null;
            try
            {
                fileIn = File.ReadAllText(fileName);
            }
            catch
            {
                Console.WriteLine("No input file");
            }
            return fileIn;
        }

        static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser<ApplicationArguments>();
            PrepareParser(parser);
            var result = parser.Parse(args);
            
            if (result.HasErrors)
            {
                Console.WriteLine("One of the parameters was missed!");
                return;
            }
            if (result.EmptyArgs || result.HelpCalled) return;
            
            var arguments = parser.Object;

            var fileIn = GetStringFromFile(arguments.FileInName);

            if (fileIn == null) return;

            string htmlLine;
            try
            {
                htmlLine = md.RenderToHtml(fileIn);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error in parsing.");
                Console.WriteLine(e.Message);
                return;
            }

            File.WriteAllText(arguments.FileOutName, htmlLine);
        }
    }
}

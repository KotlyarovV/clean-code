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

        static void PrepareParser(FluentCommandLineParser<ApplicationArguments> parser)
        {
            parser.Setup(arg => arg.FileInName)
                .As("in")
                .Required();

            parser.Setup(arg => arg.FileOutName)
                .As("out")
                .Required();
        }

        static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser<ApplicationArguments>();
            PrepareParser(parser);
            var result = parser.Parse(args);

            if (result.HasErrors)
            {
                Console.WriteLine("One of the parameters was missed!");
                return; ;
            }

            var arguments = parser.Object;

            string fileIn;

            try
            {
                fileIn = File.ReadAllText(arguments.FileInName);
            }
            catch
            {
                Console.WriteLine("No input file");
                return;
            }

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

using System;
using System.IO;
using Fclp;

namespace Markdown
{
    class Program
    {

        static Tuple<string, string, ICommandLineParserResult> ParseArgs(string[] args)
        {
            var parser = new FluentCommandLineParser();

            // можно выделить параметры в отдельный класс
            // new FluentCommandLineParser<ApplicationArguments>();
            var fileOutName = string.Empty;
            var fileInName = string.Empty; 

            // каждый раз при вызове парсинга аргументов будет занаво настраиваться парсер
            parser.Setup<string>("out")
                .Callback(str => fileOutName = str)
                .Required();    

            parser.Setup<string>("in")
                .Callback(str => fileInName = str)
                .Required();
            // прелесть этой библиотеки в том, что она позволяет находу генерировать help, а ты этим не пользуешься :(
           
            var result = parser.Parse(args);
            return Tuple.Create(fileInName, fileOutName, result);
        }

        static void Main(string[] args)
        {
            var parsingResult = ParseArgs(args);

            // parsingResult, result. давай clean-code!
            var result = parsingResult.Item3;
            if (result.HasErrors)
            {
                Console.WriteLine("One of the parameters was missed!");
                return; ;
            }

            var fileOutName = parsingResult.Item2;
            var fileInName = parsingResult.Item1;

            string fileIn;

            try
            {
                fileIn = File.ReadAllText(fileInName);
            }
            catch
            {
                Console.WriteLine("No in file"); // не экономь на спичках
                return;
            }

            string mdLine; // разве мы mdLine получаем вызвал метод RenderToHtml?
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

            File.WriteAllText(fileOutName, mdLine);
        }
    }
}

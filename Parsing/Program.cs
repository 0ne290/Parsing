using Parsing.Core.Domain.Data.StateMachine;
using Parsing.Core.Domain.Logic;

namespace Parsing;

internal static class Program
{
    private static int Main()
    {
        try
        {
            var reader = new StreamReader("input.txt", System.Text.Encoding.UTF8);

            var inputString = reader.ReadToEnd();
        
            reader.Dispose();

            var rootOfComposition = new RootOfComposition();
            var translator = rootOfComposition.CreateTranslator();
            translator.Translate(inputString);

            Console.Write("Нажмите любую клавишу для завершения программы...");
            Console.ReadKey();

            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.Write("\nНажмите любую клавишу для завершения программы...");
            Console.ReadKey();
            
            return 1;
        }
    }
}
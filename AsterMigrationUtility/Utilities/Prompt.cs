namespace AsterMigrationUtility.Utilities;

public class Prompt
{
    public static string Show(string question, string[]? options = null)
    {
        Console.WriteLine("");
        Console.WriteLine(question);

        if (options?.Length > 0)
        {
            var optionNum = 0;
            foreach (var option in options)
            {
                Console.WriteLine(" " + optionNum + "   " + option);
                optionNum++;
            }
        }
        
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(" > ");
        Console.ForegroundColor = ConsoleColor.Green;
        var response = Console.ReadLine() ?? string.Empty;
        Console.ResetColor();

        return response;
    }
}
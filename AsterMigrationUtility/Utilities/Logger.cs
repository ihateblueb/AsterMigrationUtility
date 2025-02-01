namespace AsterMigrationUtility.Utilities;

public class Logger
{
    public static void Debug(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.Write(" Debug ");
        Console.ResetColor();
        
        Console.Write(" ");
        Console.Write(message);
        Console.Write("\n");
    }
    
    public static void Warn(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.Write(" Error ");
        Console.ResetColor();
        
        Console.Write(" ");
        Console.Write(message);
        Console.Write("\n");
    }
    
    public static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Red;
        Console.Write(" Error ");
        Console.ResetColor();
        
        Console.Write(" ");
        Console.Write(message);
        Console.Write("\n");
    }

    public static void DebugMany(string[] messages)
    {
        foreach (string message in messages)
        {
            Debug(message);
        }
    }
}
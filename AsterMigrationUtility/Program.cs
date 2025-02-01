using AsterMigrationUtility;
using AsterMigrationUtility.Migrations;
using AsterMigrationUtility.Utilities;

Console.CancelKeyPress += delegate {
    Console.ResetColor();

    if (State.AllowShutdown)
    {
        Console.WriteLine("\n\nExiting...");
    }
    else
    {
        Console.WriteLine("\n\nExit blocked.");
        Console.WriteLine("Exiting now will leave your new database in a potentially corrupted state.");
        Console.WriteLine("If you're sure, try exiting again.");
        State.BypassExitBlock = true;
        
        var resetBypass = new Task(() =>
        {
            State.BypassExitBlock = false;
        });
        Task.Delay(4000);
        resetBypass.Start();
    }
};

Console.WriteLine("hello!");

var from = Prompt.Show("Where are you migrating from?", ["Iceshrimp.NET"]);

Console.WriteLine("");

if (from == "0")
{
    IceshrimpNET.Start();
}
else
{
    Console.WriteLine("failed to start migration, invalid instance software");
}
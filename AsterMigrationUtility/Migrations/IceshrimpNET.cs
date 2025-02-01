using System.Diagnostics;
using AsterMigrationUtility.Utilities;
using IniParser;
using IniParser.Model;
using Npgsql;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AsterMigrationUtility.Migrations;

public class IceshrimpNET
{
    public static void Start()
    {
        Console.WriteLine("starting migration from Iceshrimp.NET...");
        
        /* Iceshrimp.NET Config */

        var iceshrimpConfigPath = Environment.GetEnvironmentVariable("IceshrimpNETConfigPath") ?? Prompt.Show("Where is your Iceshrimp.NET config located?");

        if (!File.Exists(iceshrimpConfigPath))
        {
            Console.Error.WriteLine("Iceshrimp.NET config file could not be found.");
            return;
        }

        IniData iceshrimpConfig = new FileIniDataParser().ReadFile(iceshrimpConfigPath);

        
        var isDbConfig = new DatabaseConfig();
        isDbConfig.host = iceshrimpConfig["Database"]["Host"];
        isDbConfig.port = iceshrimpConfig["Database"]["Port"] ?? "5432";
        isDbConfig.name = iceshrimpConfig["Database"]["Database"];
        isDbConfig.user = iceshrimpConfig["Database"]["Username"];
        isDbConfig.pass = iceshrimpConfig["Database"]["Password"];
        
        Logger.DebugMany([
            "isDbConfig.host: " + isDbConfig.host,
            "isDbConfig.port: " + isDbConfig.port,
            "isDbConfig.name: " + isDbConfig.name,
            "isDbConfig.user: " + isDbConfig.user,
            "isDbConfig.pass: " + isDbConfig.pass
        ]);
        
        /* Aster Config */
        
        var asterConfigPath = Environment.GetEnvironmentVariable("AsterConfigPath") ?? Prompt.Show("Where is your Aster config located?");
        
        if (!File.Exists(asterConfigPath))
        {
            Console.Error.WriteLine("Aster config file could not be found.");
            return;
        }

        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        var asterConfig = deserializer.Deserialize<AsterConfig>(File.ReadAllText(asterConfigPath));
        
        var asterDbConfig = new DatabaseConfig();
        asterDbConfig.host = asterConfig.database.host;
        asterDbConfig.port = asterConfig.database.port.ToString() ?? "5432";
        asterDbConfig.name = asterConfig.database.name;
        asterDbConfig.user = asterConfig.database.user;
        asterDbConfig.pass = asterConfig.database.pass;
        
        Logger.DebugMany([
            "asterDbConfig.host: " + asterDbConfig.host,
            "asterDbConfig.port: " + asterDbConfig.port,
            "asterDbConfig.name: " + asterDbConfig.name,
            "asterDbConfig.user: " + asterDbConfig.user,
            "asterDbConfig.pass: " + asterDbConfig.pass
        ]);
        
        /*
         * Lines to change:
         * url
         * port (ask)
         * registrations 
         * id
         * authorizedFetch
         */

        State.AllowShutdown = false;

        Migrate(asterDbConfig, isDbConfig).Wait();
    }

    private static NpgsqlDataSource CreateNpgsqlDataSource(DatabaseConfig dbConfig)
    {
        var connString = "Host="+dbConfig.host+";Port="+dbConfig.port+";Username="+dbConfig.user+";Password="+dbConfig.pass+";Database="+dbConfig.name+"";
           
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
        var dataSource = dataSourceBuilder.Build();

        return dataSource;
    }

    private static async Task Migrate(DatabaseConfig asterDbConfig, DatabaseConfig isDbConfig)
    {
        var isDataSource = CreateNpgsqlDataSource(isDbConfig);
        await isDataSource.OpenConnectionAsync();
        
        var asDataSource = CreateNpgsqlDataSource(asterDbConfig);
        await asDataSource.OpenConnectionAsync();
        
        
        /*
         *  Checking state of Aster database
         * This gets the latest migration ID based off of the last existing
         * migration at the time of the last update to this script.
         * If any more migrations exist, the user will be warned.
         */

        if (Environment.GetEnvironmentVariable("BypassMigrationCheck") == "1")
        {
            Logger.Warn("Bypassing migration check.");
        }
        else
        {
            var migrationCheck = asDataSource.CreateCommand("SELECT id FROM migrations WHERE name='Migration1738369585590';");
            var lastKnownMigrationId = 0;
            await using (var reader = await migrationCheck.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                    lastKnownMigrationId = reader.GetInt32(0);
            }
        
            var checkIfMoreMigrationsExist = asDataSource.CreateCommand("SELECT id FROM migrations ORDER BY id DESC LIMIT 1;");
            var lastMigrationId = 0;
            await using (var reader = await checkIfMoreMigrationsExist.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                    lastMigrationId = reader.GetInt32(0);
            }
        
            if (lastMigrationId > lastKnownMigrationId)
            {
                Logger.Error(
                    "There are migrations that exist after this script has last been updated. Check if this is a problem or not before continuing. If you'd like to continue, rerun the script with BypassMigrationCheck=1 set.");
                return;
            }
        }
        
        
    }
}
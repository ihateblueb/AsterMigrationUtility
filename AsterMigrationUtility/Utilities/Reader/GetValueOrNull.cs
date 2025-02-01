
using Npgsql;

namespace AsterMigrationUtility.Utilities.Reader;

public class GetValueOrNull
{
    public static string? String(NpgsqlDataReader reader, string ordinalName)
    {
        var ordinal = reader.GetOrdinal(ordinalName);
        return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
    }
    
    public static string[]? StringArray(NpgsqlDataReader reader, string ordinalName)
    {
        var ordinal = reader.GetOrdinal(ordinalName);
        return reader.IsDBNull(ordinal) ? null : (string[])reader.GetValue(ordinal);
    }
}
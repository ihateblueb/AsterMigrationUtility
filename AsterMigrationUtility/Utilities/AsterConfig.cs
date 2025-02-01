namespace AsterMigrationUtility.Utilities;

public class AsterConfig
{
    public string? url { get; set; }
    public int? port { get; set; }
    
    public string? registrations { get; set; }
    public string? id { get; set; }
    public string? locale { get; set; }
    public string? authorizedFetch { get; set; }
    
    public database? database { get; set; }
}

public class database
{
    public string? host { get; set; }
    public int? port { get; set; }
    public string? name { get; set; }
    public string? user { get; set; }
    public string? pass { get; set; }
}
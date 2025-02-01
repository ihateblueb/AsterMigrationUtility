namespace AsterMigrationUtility.Utilities;

public class AsterNote
{
    public string id { get; set; }
    public string apId { get; set; }
    
    public string userId { get; set; }
    public string? cw { get; set; }
    public string? content { get; set; }
    public string visibility { get; set; }
    
    public string? replyingToId { get; set; }
    public string? pollId { get; set; }
    public string? repeatId { get; set; }
    public string[]? to { get; set; }
    
    public string[]? attachments { get; set; }
    public string[]? emojis { get; set; }
    
    public string createdAt { get; set; }
    public string? updatedAt { get; set; }
    
    public string[]? repeatIds { get; set; }
    public string[]? likeIds { get; set; }
    public string[]? reactionIds { get; set; }
}
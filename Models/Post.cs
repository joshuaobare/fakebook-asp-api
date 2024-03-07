using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace fakebook_asp_api.Models;

public class Post {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]    
    public string UserId { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string[] Likes { get; set; } = Array.Empty<string>();
}

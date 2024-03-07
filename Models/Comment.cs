using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fakebook_asp_api.Models;

public class Comment {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CommentId { get; set; }
    public string Text { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string PostId { get; set; } = null!;
}

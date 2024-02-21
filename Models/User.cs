using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fakebook_asp_api.Models;

public class User{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }
    public string Username { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime JoinedAt { get; set; } = DateTime.Now;
    public string Avatar { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public string JobTitle { get; set; } = null!;
    public string HomeLocation { get; set; } = null!;
    public string RelationshipStatus { get; set; } = null!;
    public string[] Friends { get; set; } = Array.Empty<string>();
    public string[] FriendRequests { get; set; } = Array.Empty<string>();
}

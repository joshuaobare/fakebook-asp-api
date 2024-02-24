using Microsoft.Extensions.Options;
using MongoDB.Driver;
using fakebook_asp_api.Models;

namespace fakebook_asp_api.Services;

public class PostService {
    private readonly IMongoCollection<Post> _postCollection;

    public PostService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.MongoDBConnection);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _postCollection = mongoDatabase.GetCollection<Post>(databaseSettings.Value.CollectionName);
    }
    public async Task<List<Post>> GetPostsAsync() =>
        await _postCollection.Find(_ => true).ToListAsync();

    public async Task<Post?> GetPostAsync(string id) =>
        await _postCollection.Find(x => x.PostId == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Post newPost) =>
        await _postCollection.InsertOneAsync(newPost);

    public async Task UpdateAsync(string id, Post updatedPost) =>
        await _postCollection.ReplaceOneAsync(x => x.PostId == id, updatedPost);

    public async Task RemoveAsync(string id) =>
        await _postCollection.DeleteOneAsync(x => x.PostId == id);
}

public class CommentService
{
    private readonly IMongoCollection<Comment> _commentCollection;

    public CommentService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.MongoDBConnection);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _commentCollection = mongoDatabase.GetCollection<Comment>(databaseSettings.Value.CollectionName);
    }

    public async Task<List<Comment>> GetCommentsAsync() =>
        await _commentCollection.Find(_ => true).ToListAsync();
    public async Task<Comment?> GetCommentAsync(string id) =>
        await _commentCollection.Find(x => x.CommentId == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Comment newComment) =>
        await _commentCollection.InsertOneAsync(newComment);

    public async Task UpdateAsync(string id, Comment updatedComment) =>
        await _commentCollection.ReplaceOneAsync(x => x.CommentId == id, updatedComment);

    public async Task RemoveAsync(string id) =>
        await _commentCollection.DeleteOneAsync(x => x.CommentId == id);
}

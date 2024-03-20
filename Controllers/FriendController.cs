using Microsoft.AspNetCore.Mvc;
using fakebook_asp_api.Models;
using fakebook_asp_api.Services;
using MongoDB.Driver;
namespace fakebook_asp_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FriendController : Controller
{
    private readonly UserService _userService;

    public FriendController(UserService userService) => _userService = userService;

    public async Task<IActionResult> SendRequest(string friendId,string userId)
    {
        
        var filter = Builders<Users>.Filter.Eq(user => user.UserId, friendId);
        var update = Builders<Users>.Update.Push<String>(e => e.FriendRequests, userId);

        await _userService.UpdateAsync(filter,update);

        return NoContent();
    }

    public async Task<IActionResult> DeleteRequest(string friendId, string userId)
    {
        var filter = Builders<Users>.Filter.Eq(user => user.UserId, friendId);
        var update = Builders<Users>.Update.Pull<String>(e => e.FriendRequests, userId);

        await _userService.UpdateAsync(filter, update);

        return NoContent();
    }

    public async Task<IActionResult> AcceptRequest(string friendId, string userId)
    {
        var userFilter = Builders<Users>.Filter.Eq(user => user.UserId, userId);
        var userPullUpdate = Builders<Users>.Update.Pull<String>(e => e.FriendRequests, userId);
        var userPushUpdate = Builders<Users>.Update.Push<String>(e => e.Friends, friendId);
        var update = Builders<Users>.Update.Combine(userPullUpdate, userPushUpdate);

        await _userService.UpdateAsync(filter, update);

        return NoContent();
    }

    public async Task<IActionResult> RemoveFriend()
    {

    }
}

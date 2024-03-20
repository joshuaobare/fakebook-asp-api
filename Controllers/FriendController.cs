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
        var user = await _userService.GetUserAsync(friendId);

        if (user is null) 
        {
            return NotFound();
        }

        var filter = Builders<Users>.Filter.Eq(user => user.UserId, friendId);
        var update = Builders<Users>.Update.Push<String>(e => e.FriendRequests, userId);

        await _userService.UpdateAsync(filter,update);

        return NoContent();
    }

    public async Task<IActionResult> DeleteRequest()
    {

    }

    public async Task<IActionResult> AcceptRequest()
    {

    }

    public async Task<IActionResult> RemoveFriend()
    {

    }
}

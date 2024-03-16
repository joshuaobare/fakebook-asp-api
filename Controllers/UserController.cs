using Microsoft.AspNetCore.Mvc;
using fakebook_asp_api.Models;
using fakebook_asp_api.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;


namespace fakebook_asp_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller {
    private readonly UserService _userService;

    public UserController(UserService userService) => _userService = userService;

    [HttpGet]
    public async Task<List<Users>> Get() =>
        await _userService.GetUsersAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Users>> Get(string id)
    {
        var user = await _userService.GetUserAsync(id);

        if (user is null)
        {
            return NotFound();
        }
        return user;

    }

    [HttpPost]
    public async Task<IActionResult> Post(Users newUser)
    {
        // generate a 128-bit salt using a secure PRNG
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: newUser.Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        await _userService.CreateAsync(new Users { UserId = newUser.UserId, Password = hashed, IsAdmin = newUser.IsAdmin, Username = newUser.Username, FullName = newUser.FullName, Email = newUser.Email , JoinedAt = newUser.JoinedAt, Avatar = newUser.Avatar, Bio = newUser.Bio, JobTitle = newUser.JobTitle, HomeLocation = newUser.HomeLocation, RelationshipStatus = newUser.RelationshipStatus, Friends = newUser.Friends, FriendRequests = newUser.FriendRequests});
        //await _userService.CreateAsync(new Users { Password = hashed });
        return CreatedAtAction(nameof(Get), new { id = newUser.UserId }, newUser);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(Users userData)
    {
        
        var user = await _userService.GetUserUsernameAsync(userData.Username);
        if (user is null)
        {
            return NotFound();
        }
        if (user.Username == userData.Username && user.Password == userData.Password)
        {
            return CreatedAtAction(nameof(Login), user); ;
        }
        return NotFound();

    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Users updatedUser)
    {
        var user = await _userService.GetUserAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.UserId = user.UserId;

        await _userService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userService.GetUserAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.RemoveAsync(id);

        return NoContent();
    }

}

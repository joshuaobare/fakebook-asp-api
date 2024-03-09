using Microsoft.AspNetCore.Mvc;
using fakebook_asp_api.Models;
using fakebook_asp_api.Services;


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
        await _userService.CreateAsync(newUser);

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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Services.Interfaces;
using TaskManager.ViewModels.UserTheme;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserThemeController : ControllerBase
{
    private readonly IUserThemeService _service;

    public UserThemeController(IUserThemeService service)
    {
        _service = service;
    }


    [HttpPost]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> AddUserToTask(CreateUserThemeVM vm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _service.AddUsersToTheme(vm);
        if (result.StatusCode == Enum.StatusCode.OK)
            return Ok(result);

        return BadRequest(result);
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> RemoveUserFromTheme(long id)
    {
        var result = await _service.RemoveUserFromTheme(id);
        if (result.StatusCode == Enum.StatusCode.OK)
            return Ok(result);
        return BadRequest(result);
    }


    [HttpGet("Theme/{themeId}/Users")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetUsersByThemeId(long themeId)
    {
        var users = await _service.GetUsersByThemeId(themeId);
        if (users.StatusCode == Enum.StatusCode.OK)
            return Ok(users);
        return BadRequest(users);
    }


    [HttpGet("User/{userId}/Theme")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetThemesByUserId(long userId)
    {
        var users = await _service.GetThemesByUserId(userId);
        if (users.StatusCode == Enum.StatusCode.OK)
            return Ok(users);
        return BadRequest(users);
    }
}

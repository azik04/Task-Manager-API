using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Dto.UserTheme;
using TaskManager.Core.Interfaces;

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
    public async Task<IActionResult> AddUserToTheme(CreateUserThemeDto vm)
    {
        var result = await _service.CreateAsync(vm);
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }


    [HttpDelete("Theme/{themeId}/User/{userId}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> RemoveUserFromTheme(long id)
    {
        var result = await _service.RemoveAsync(id);
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }


    [HttpGet("Theme/{themeId}/Users")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetUsersByThemeId(long themeId)
        {
        var result = await _service.GetUsersAsync(themeId);
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }


    [HttpGet("User/{userId}/Theme")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetThemesByUserId(long userId)
    {
        var result = await _service.GetThemeAsync(userId);
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
}

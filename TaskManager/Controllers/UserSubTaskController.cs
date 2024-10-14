using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Services.Interfaces;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserSubTaskController : ControllerBase
{
    private readonly IUserSubTaskService _service;

    public UserSubTaskController(IUserSubTaskService service)
    {
        _service = service;
    }
    [HttpPost("{subTaskId}/users/{userId}")]
    [Authorize(Policy = "User")]

    public async Task<IActionResult> AddUserToSubTask(long subTaskId, long userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _service.AddUsersToSubTask(subTaskId, userId);
        if (result)
            return Ok("User added to task.");
        return BadRequest("Failed to add user to task.");
    }

    [HttpDelete("{taskId}/users/{userId}")]
    [Authorize(Policy = "User")]

    public async Task<IActionResult> RemoveUserFromSubTask(long subTaskId, long userId)
    {
        var result = await _service.RemoveUserFromSubTask(subTaskId, userId);
        if (result)
            return Ok("User removed from task.");
        return BadRequest("Failed to remove user from task.");
    }

    [HttpGet("{taskId}/users")]
    [Authorize(Policy = "User")]

    public async Task<IActionResult> GetUsersBySubTaskId(long subTaskId)
    {
        var users = await _service.GetUsersBySubTaskkId(subTaskId);
        return Ok(users);
    }
    [HttpGet("{userId}/theme")]
    [Authorize(Policy = "User")]

    public async Task<IActionResult> GetSubTaskByUserId(long userId)
    {
        var users = await _service.GetSubTaskByUserId(userId);
        return Ok(users);
    }
}

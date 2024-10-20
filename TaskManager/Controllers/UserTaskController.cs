using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Context;
using TaskManager.Services.Interfaces;
using TaskManager.ViewModels.UserTask;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTaskController : ControllerBase
{
    private readonly IUserTaskService _service;
    private readonly ApplicationDbContext _db;
    public UserTaskController(IUserTaskService service, ApplicationDbContext db)
    {
        _service = service;
        _db = db;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var data = _db.UserTasks.ToList();
        return Ok(data);
    }

    [HttpPost]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> AddUserToTask(CreateUserTaskVM vm) 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _service.AddUsersToTask(vm); 
        if (result.StatusCode == Enum.StatusCode.OK) 
            return Ok(result);

        return BadRequest(result);
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> RemoveUserFromTask(long id)
    {
        var result = await _service.RemoveUserFromTask(id);     
        if (result.StatusCode == Enum.StatusCode.OK)
            return Ok(result);
        return BadRequest(result);
    }


    [HttpGet("Task/{taskId}/Users")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetUsersByTaskId(long taskId)
    {
        var users = await _service.GetUsersByTaskId(taskId);
        if (users.StatusCode == Enum.StatusCode.OK)
            return Ok(users);
        return BadRequest(users);
    }


    [HttpGet("User/{userId}/Task")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetTaskByUserId(long userId)
    {
        var users = await _service.GetTasksByUserId(userId);
        if(users.StatusCode == Enum.StatusCode.OK)
            return Ok(users);
        return BadRequest(users);
    }
}

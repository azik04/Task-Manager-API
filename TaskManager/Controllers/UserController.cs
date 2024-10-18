using Microsoft.AspNetCore.Mvc;
using TaskManager.Services.Interfaces;
using TaskManager.ViewModels.UsersVMs;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        _service = service;
    }


    [HttpPost("LogIn")]
    public async Task<IActionResult> LogIn(LogInVM task)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var res = await _service.LogIn(task);
        if (res.StatusCode == Enum.StatusCode.OK)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _service.GetAll();
        if (res.StatusCode == Enum.StatusCode.OK)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetById(long id)
    {
        var res = await _service.GetById(id);
        return Ok(res);
    }



    [HttpPost("LogOut")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> LogOut()
    {
        return Ok("LogOut successfully");
    }
}

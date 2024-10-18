using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Services.Interfaces;
using TaskManager.ViewModels.RegisterVM;
using TaskManager.ViewModels.UsersVMs;

namespace TaskManager.Areas.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
[Area("Admin")]
public class AdminController : ControllerBase
{
    private readonly IUserService _service;
    public AdminController(IUserService service)
    {
        _service = service;
    }


    [HttpPost("Register")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Register(RegisterVM task)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var res = await _service.Register(task);
        if (res.StatusCode == Enum.StatusCode.OK)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet("Admins")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var res = await _service.GetAllAdmins();
        if (res.StatusCode == Enum.StatusCode.OK)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet("User")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var res = await _service.GetAllUsers();
        if (res.StatusCode == Enum.StatusCode.OK)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _service.GetAll();
        if (res.StatusCode == Enum.StatusCode.OK)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpPut("{id}/ChangeRole")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> ChangeRole(long id)
    {
        var res = await _service.ChangeRole(id);
        if (res.StatusCode == Enum.StatusCode.OK)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpPut("{id}/ChangePassword")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> ChangePassword(long id, ChangePasswordVM changePassword)
    {
        var res = await _service.ChangePassword(id, changePassword);
        if (res.StatusCode == Enum.StatusCode.OK)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpPut("{id}/ChangeEmail")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> ChangeEmail(long id, ChangeEmailVM changeEmail)
    {
        var res = await _service.ChangeEmail(id, changeEmail);
        if (res.StatusCode == Enum.StatusCode.OK)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.Remove(id);
        if (res.StatusCode == Enum.StatusCode.OK)
            return Ok(res);

        return BadRequest(res);
    }
}

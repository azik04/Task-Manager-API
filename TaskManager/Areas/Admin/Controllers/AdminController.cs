using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Dto.Users;
using TaskManager.Core.Interfaces;
using TaskManager.DataProvider.Enums;

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
    public async Task<IActionResult> Register(CreateUserDto task)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var res = await _service.Create(task);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet("Admins")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var res = await _service.GetAdmin();
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet("User")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var res = await _service.GetUser();
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpPut("{id}/ChangeRole")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> ChangeRole(long id , Role role)
    {
        var res = await _service.ChangeRole(id, role);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    //[HttpPut("{id}/ChangePassword")]
    //[Authorize(Policy = "Admin")]
    //public async Task<IActionResult> ChangePassword(long id, ChangePasswordDto changePassword)
    //{
    //    var res = await _service.ChangePassword(id, changePassword);
    //    if (res.StatusCode == Enum.StatusCode.OK)
    //        return Ok(res);

    //    return BadRequest(res);
    //}


    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.Remove(id);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }
}

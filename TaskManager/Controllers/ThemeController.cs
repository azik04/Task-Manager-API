using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Dto.Themes;
using TaskManager.Core.Interfaces;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ThemeController : ControllerBase
{
    private readonly IThemeService _service;
    public ThemeController(IThemeService service)
    {
        _service = service;
    }


    [HttpPost]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Create(CreateThemeDto task)
    {
        var res = await _service.CreateAsync(task);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetAll(long userId)
    {
        var res = await _service.GetAllAsync(userId);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.RemoveAsync(id);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpPut("/{id}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Update(long id, UpdateThemeDto dto)
    {
        var res = await _service.UpdateAsync(id, dto);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Dto.Tasks;
using TaskManager.Core.Interfaces;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;
    public TaskController(ITaskService service)
    {
        _service = service;
    }


    [HttpPost]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Create(CreateTaskDto task)
    {
        var res = await _service.Create(task);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet("Done")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetAllDone(long themeId)
    {
        var res = await _service.GetAllDone(themeId);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet("NotDone")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetAllNotDone(long themeId)
    {
        var res = await _service.GetAllNotDone(themeId);
        if (res.Success)
            return Ok(res.Data);

        return BadRequest(res);
    }


    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetById(long id)
    {
        var res = await _service.GetById(id);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Remove(long id)
    {
        var res = await _service.Remove(id);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpPut("{id}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Update(long id, UpdateTaskDto task)
    {
        var res = await _service.Update(id, task);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpPut("{id}/Complite")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Complite(long id)
    {
        var res = await _service.Complite(id);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Dto.SubTask;
using TaskManager.Core.Interfaces;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubTaskController : ControllerBase
{
    private readonly ISubTaskService _service;
    public SubTaskController(ISubTaskService service)
    {
        _service = service;
    }


    [HttpPost]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Create(CreateSubTaskDto subTask)
    {
        var res = await _service.Create(subTask);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpGet("Task/{taskId}/NotDone")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetByTaskNotDone(long TaskId)
    {
        var res = await _service.GetByTaskNotDone(TaskId);
        if (res.Success)
            return Ok(res);

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


    [HttpGet("Task/{taskId}/Done")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetByTaskDone(long TaskId)
    {
        var res = await _service.GetByTaskDone(TaskId);
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


    [HttpPut("{id}/Complite")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Complete(long id)
    {
        var res = await _service.Complete(id);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }



    [HttpPut]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Update(long taskId , UpdateSubTaskDto dto)
    {
        var res = await _service.Update(taskId, dto);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }
}

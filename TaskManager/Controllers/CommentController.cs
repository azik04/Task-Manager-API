using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Dto.Comment;
using TaskManager.Core.Interfaces;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _service;

    public CommentController(ICommentService service)
    {
        _service = service;
    }


    [HttpGet("Task/{taskId}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetByTask(long taskId)
    {
        var res = await _service.GetByTask(taskId);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }


    [HttpPut("Task/{taskId}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Update(long taskId , UpdateCommentDto dto)
    {
        var res = await _service.Update(taskId , dto);
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


    [HttpPost]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> Create(CreateCommentDto comment)
    {
        var res = await _service.Create(comment);
        if (res.Success)
            return Ok(res);

        return BadRequest(res);
    }
}

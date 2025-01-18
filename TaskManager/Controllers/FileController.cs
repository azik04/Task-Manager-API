using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Dto.Files;
using TaskManager.Core.Interfaces;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }


    [HttpPost("Upload")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> UploadFile([FromForm] CreateFileDto uploadFile)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (uploadFile.File == null || uploadFile.TaskId <= 0)
            return BadRequest("Invalid file or task ID.");

        var result = await _fileService.UploadFile(uploadFile.File.OpenReadStream(), uploadFile);
        if (result.Success)
            return Ok("File uploaded successfully.");

        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading file.");
    }


    [HttpGet("{id}/Download")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> DownloadFile(long id)
    {
        var fileResult = await _fileService.DownloadFile(id);
        if (fileResult == null)
            return NotFound("File not found.");

        return File(fileResult.FileStream, fileResult.ContentType, fileResult.FileDownloadName);
    }



    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> DeleteFile(long id)
    {
        var result = await _fileService.DeleteFile(id);
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }


    [HttpGet("Task/{taskId}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> ListFiles(long taskId)
    {
        var files = await _fileService.ListFilesAsync(taskId);
        if (files.Success)
            return Ok(files);

        return BadRequest(files);
    }
}

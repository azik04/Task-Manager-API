using System.Web.Mvc;
using TaskManager.Core.Dto.Files;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Interfaces;

public interface IFileService
{
    Task<BaseResponse<GetFileDto>> UploadFile(Stream fileStream, CreateFileDto dto);
    Task<FileStreamResult> DownloadFile(long id);
    Task<BaseResponse<ICollection<GetFileDto>>> ListFilesAsync(long taskId);
    Task<BaseResponse<GetFileDto>> DeleteFile(long id);
}

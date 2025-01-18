using TaskManager.DataProvider.Enums;

namespace TaskManager.Core.Dto.SubTask;

public class UpdateSubTaskDto
{
    public string Name { get; set; }
    public Priority Priority { get; set; }
    public DateOnly DeadLine { get; set; }
}

using TaskManager.DataProvider.Enums;

namespace TaskManager.Core.Dto.Tasks;

public class UpdateTaskDto
{
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public DateOnly DeadLine { get; set; }
    public string Contact { get; set; }

    public long? ExecutiveUserId { get; set; }
    public ICollection<long> UserTasks { get; set; }
}

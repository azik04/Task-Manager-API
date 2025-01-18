using TaskManager.DataProvider.Enums;

public class CreateTaskDto
{
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public DateOnly? DeadLine { get; set; }
    public long ThemeId { get; set; }
    public string? Contact { get; set; }
    public long? ExecutiveUserId { get; set; }
    public IList<long> UserId { get; set; }
}

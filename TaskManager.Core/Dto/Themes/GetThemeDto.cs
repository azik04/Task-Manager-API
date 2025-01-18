namespace TaskManager.Core.Dto.Themes;

public class GetThemeDto
{
    public long Id { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public string Name { get; set; }
    public long CreatedBy { get; set; }


}

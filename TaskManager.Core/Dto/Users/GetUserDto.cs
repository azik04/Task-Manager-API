namespace TaskManager.Core.Dto.Users;
public class GetUserDto
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime CreateAt { get; set; }
    public bool isDeleted { get; set; }
}

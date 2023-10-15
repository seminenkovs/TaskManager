namespace TaskManagerApi.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public byte[] Photo { get; set; }
    public UserStatus Status { get; set; }

    public List<Project> Projects { get; set; } = new List<Project>();
    public List<Desk> Desks { get; set; } = new List<Desk>();
    public List<Task> Tasks { get; set; } = new List<Task>();
}
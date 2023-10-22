using TaskManager.Common.Models;

namespace TaskManagerApi.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Phone { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public byte[]? Photo { get; set; }
    public UserStatus Status { get; set; }

    public List<Project> Projects { get; set; } = new List<Project>();
    public List<Desk> Desks { get; set; } = new List<Desk>();
    public List<Task> Tasks { get; set; } = new List<Task>();

    public User() { }

    public User(string firstName, string lastName, string email, string password,
        UserStatus status = UserStatus.User, string? phone = null, byte[]? photo = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Phone = phone;
        RegistrationDate = DateTime.Now;
        Photo = photo;
        Status = status;
    }

    public UserModel ToDto()
    {
        return new UserModel()
        {
            Id = this.Id,
            FirstName = this.FirstName,
            LastName = this.LastName,
            Email = this.Email,
            Password = this.Password,
            Phone = this.Phone,
            RegistrationDate = this.RegistrationDate,
            Status = this.Status,
            Photo = this.Photo
    };
    }
}
using System.Text;
using TaskManagerApi.Models.Data;

namespace TaskManagerApi.Models.Services;

public class UserServices
{
    private readonly ApplicationContext _db;

    public UserServices(ApplicationContext db)
    {
        _db = db;
    }

    public Tuple<string, string> GetUserLoginPassFromBasicAuth(HttpRequest request)
    {
        string userName = string.Empty;
        string userPass = string.Empty;
        string authHeader = request.Headers["Authorization"].ToString();
        if (authHeader != null && authHeader.StartsWith("Basic"))
        {
            string endcodedUserNamePass = authHeader.Replace("Basic", "");
            var encoding = Encoding.GetEncoding("iso-8859-1");

            string[] nemePassArray = encoding.GetString(Convert.FromBase64String(endcodedUserNamePass)).Split(':');
            userName = nemePassArray[0];
            userPass = nemePassArray[1];
        }

        return new Tuple<string, string>(userName, userPass);
    }

    public User GetUser(string login, string password)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
        return user;
    }
}
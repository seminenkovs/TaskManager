using System.Security.Claims;
using System.Text;
using TaskManager.Common.Models;
using TaskManagerApi.Models.Data;

namespace TaskManagerApi.Models.Services;

public class UserService : ICommonService<UserModel>
{
    private readonly ApplicationContext _db;

    public UserService(ApplicationContext db)
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

    public ClaimsIdentity GetIdentity(string username, string password)
    {
        User currentUser = GetUser(username, password);
        if (currentUser != null)
        {
            currentUser.LastLoginDate = DateTime.Now;
            _db.Users.Update(currentUser);
            _db.SaveChanges();

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, currentUser.Status.ToString())
            };
            ClaimsIdentity claimsIdentity = 
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        return null;
    }

    public bool Create(UserModel model)
    {
        try
        {
            User newUser = new User(model.FirstName, model.LastName,
                model.Email, model.Password, model.Status,
                model.Phone, model.Photo);

            _db.Users.Add(newUser);
            _db.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }
        

        return true;
    }

    public bool Update(int id, UserModel model)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id, UserModel model)
    {
        throw new NotImplementedException();
    }
}
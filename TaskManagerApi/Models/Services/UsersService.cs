using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using TaskManager.Common.Models;
using TaskManagerApi.Models.Data;

namespace TaskManagerApi.Models.Services;

public class UsersService : AbstractionService, ICommonService<UserModel>
{
    private readonly ApplicationContext _db;

    public UsersService(ApplicationContext db)
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
        return DoAction(delegate()
        {
            User newUser = new User(model.FirstName, model.LastName,
                model.Email, model.Password, model.Status,
                model.Phone, model.Photo);

            _db.Users.Add(newUser);
            _db.SaveChanges();
        });
    }

    public bool Update(int id, UserModel model)
    {
        User userForUpdate = _db.Users.FirstOrDefault(u => u.Id == id);
        if (userForUpdate != null)
        {
            return DoAction(delegate()
            {
                userForUpdate.FirstName = model.FirstName;
                userForUpdate.LastName = model.LastName;
                userForUpdate.Password = model.Password;
                userForUpdate.Phone = model.Phone;
                userForUpdate.Photo = model.Photo;
                userForUpdate.Status = model.Status;
                userForUpdate.Email = model.Email;

                _db.Users.Update(userForUpdate);
                _db.SaveChanges();
            });
        }

        return false;
    }

    public bool Delete(int id)
    {
        User userForDelete = _db.Users.FirstOrDefault(u => u.Id == id);
        if (userForDelete != null)
        {
            return DoAction(delegate ()
            {
                _db.Users.Remove(userForDelete);
                _db.SaveChanges();
            });
        }

        return false;
    }

    public bool CreateMultipleUsers(List<UserModel> userModels)
    {
        return DoAction(delegate()
        {
            var newUsers = userModels.Select(u => new User(u));
            _db.Users.AddRange(newUsers);
            _db.SaveChangesAsync();
        });
    }
}
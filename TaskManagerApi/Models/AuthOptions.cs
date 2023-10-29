using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskManagerApi.Models;

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // Token Issuer
    public const string AUDIENCE = "MyAuthClient"; // Token User
    private const string KEY = "mysupersecret_secretkey!123"; //Encoding key
    public const int LIFETIME = 1; // tokens life time 1 min.

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}
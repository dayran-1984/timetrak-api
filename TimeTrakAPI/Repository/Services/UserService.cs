namespace TimeTrakAPI.Repository.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimeTrakAPI.Context;
using TimeTrakAPI.Entities;
using TimeTrakAPI.Helpers;
using TimeTrakAPI.Models;
using TimeTrakAPI.Repository.Contract;

public class UserService : IUserService
{
    private readonly AppSettings _appSettings;
    private readonly TimeTrakContext _context;

    public UserService(IOptions<AppSettings> appSettings, TimeTrakContext context)
    {
        _appSettings = appSettings.Value;
        _context = context;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(user => user.Username.ToLower() == model.Username.ToLower() &&
                                                          user.Password == SecurityManager.Encrypt(model.Password));

        // return null if user not found
        if (user == null)
            return new AuthenticateResponse { Error = true, Message = "Username or password is incorrect." };

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse { Result = new AuthenticateModel(user, token) };
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User GetById(int id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id);
        if (user != null)
            return user;
        else
            return new User();
    }

    // helper methods

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
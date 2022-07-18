using TimeTrakAPI.Entities;
using TimeTrakAPI.Models;

namespace TimeTrakAPI.Repository.Contract
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}

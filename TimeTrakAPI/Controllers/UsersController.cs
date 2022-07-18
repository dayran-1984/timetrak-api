namespace TimeTrakAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using TimeTrakAPI.Helpers;
using TimeTrakAPI.Models;
using TimeTrakAPI.Repository.Contract;

[ApiController]
[Route("[controller]/[action]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost()]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response.Error)
            return BadRequest(response);

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
}
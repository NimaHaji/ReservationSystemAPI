using Application.DTO_s.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api;

[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterUser registerUser)
    {
        var res=await _service.RegisterUserAsync(registerUser);
        return Ok(res);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginUser loginUser)
    {
        var res=await _service.LoginUserAsync(loginUser);
        return Ok(res);
    }

    [HttpGet]
    [Route("JwtToken")]
    public string JwtToken()
    {
       return Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
    }
}
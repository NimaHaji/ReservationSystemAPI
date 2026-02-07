using Application.DTO_s.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUser request)
    {
        try
        {
            var token = await _service.LoginUserAsync(request);
            return Ok(token);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
    
    [HttpGet]
    [Route("ViewUsers")]
    [Authorize(Roles =  "Admin")]
    public async Task<IActionResult> ViewUsers()
    {
        var res= await _service.GetAllUsersAsync();
        return Ok(res);
    }
}
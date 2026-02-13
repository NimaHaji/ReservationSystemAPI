using Application.Features.Auth.DTOs;
using Application.Features.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterUser registerUser)
    {
        var res=await _service.RegisterUserAsync(registerUser);
        return Ok(res);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUser request)
    {
        return Ok(await _service.LoginUserAsync(request));
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest dto)
    {
        return Ok(await _service.RefreshTokenAsync(dto.RefreshToken));
    }
    [HttpGet("ViewUsers")]
    [Authorize(Roles =  "Admin")]
    public async Task<IActionResult> ViewUsers()
    {
        var res= await _service.GetAllUsersAsync();
        return Ok(res);
    }
    //Logout not implemented
    //Password forget not implemented
    //Profile(jwt based) not implemented
}
using Application.DTO_s.Service;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api;
[ApiController]
[Route("api/[controller]")]
public class ServiceController:ControllerBase
{
    private readonly IServiceAppService _serviceAppService;

    public ServiceController(IServiceAppService serviceAppService)
    {
        _serviceAppService = serviceAppService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateService service)
    {
        var id=await _serviceAppService.CreateServiceAsync(service);
        return Ok(id);
    }
}
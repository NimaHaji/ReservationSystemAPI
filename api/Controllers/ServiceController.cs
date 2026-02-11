using Application.Features.Service.DTOs;
using Application.Features.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
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
    [Authorize(Roles =  "Admin")]
    public async Task<IActionResult> Create(CreateService service)
    {
        var id=await _serviceAppService.CreateServiceAsync(service);
        return Ok(id);
    }

    [HttpPatch("Edit")]
    public async Task<IActionResult> Edit(Guid serviceId,EditService service)
    {
        var res= await _serviceAppService.EditServiceAsync(serviceId,service);
        return Ok(res);
    }

    [HttpGet("ViewServices")]
    public async Task<List<ViewServices>> ViewServices()
    {
        return await _serviceAppService.ViewAllServicesAsync();
    }
}
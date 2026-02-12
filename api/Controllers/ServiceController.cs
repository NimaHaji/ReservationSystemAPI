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

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateService service)
    {
        var res=await _serviceAppService.CreateServiceAsync(service);
        return Ok(res);
    }

    [HttpPatch("{serviceId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit([FromRoute] Guid serviceId,[FromBody] EditService service)
    {
        var res= await _serviceAppService.EditServiceAsync(serviceId,service);
        return Ok(res);
    }
    
    [HttpGet("ViewServices")]
    [Authorize]
    public async Task<List<ViewServices>> ViewServices()
    {
        return await _serviceAppService.ViewAllServicesAsync();
    }

    [HttpDelete("{serviceId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] Guid serviceId)
    {
       var res = await _serviceAppService.DeleteServiceAsync(serviceId);
       return Ok(res);
    }
    
}
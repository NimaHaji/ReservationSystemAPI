using Application;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api;
[ApiController]
[Route("api/[controller]")]
public class AppointmentController:ControllerBase
{
    private readonly IAppointmentService _service;

    public AppointmentController(IAppointmentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointment dto)
    {
        var id=await _service.CreateAppointmentAsync(dto);
        return Ok(id);
    }

    [HttpPost]
    [Route("Cancel")]
    public async Task<IActionResult> Cancel(CancelAppointment appointment)
    {
        var result=await _service.DeleteAppointmentAsync(appointment.AppointmentID);
        return Ok(result);
    }
}
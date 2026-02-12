using Application.Features.Appointments.DTOs;
using Application.Features.Appointments.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
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
    [Authorize]
    public async Task<IActionResult> Create([FromBody]CreateAppointment dto)
    {
        var res=await _service.CreateAppointmentAsync(dto);
        return Ok(res);
    }

    [HttpDelete("{appointmentId}")]
    [Authorize]
    public async Task<IActionResult> Cancel([FromRoute]Guid appointmentId)
    {
        var result=await _service.CancelAppointmentAsync(appointmentId);
        return Ok(result);
    }

    [HttpGet]
    [Route("ViewAppointments")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ViewAppointments()
    {
        var appointments=await _service.ViewAppointments();
        return Ok(appointments);
    }

    [HttpGet]
    [Route("ViewMyAppointments")]
    [Authorize]
    public async Task<IActionResult> ViewMyAppointments()
    {
        var myappointments=await _service.ViewAppointments();
        return Ok(myappointments);
    }
    [HttpPatch("{appointmentId}")]
    [Authorize]
    public async Task<IActionResult> Edit([FromRoute]Guid appointmentId,[FromBody] EditAppointment appointment)
    {
        var res= await _service.UpdateAppointmentAsync(appointmentId,appointment);
        return Ok(res);
    }
}
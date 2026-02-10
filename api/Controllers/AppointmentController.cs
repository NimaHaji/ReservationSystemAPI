using Application;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    [Route("Create")]
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
    public async Task<IActionResult> ViewMyAppointments(Guid userId)
    {
        var myappointments=await _service.ViewAppointments(userId);
        return Ok(myappointments);
    }
    [HttpPatch("Edit")]
    [Authorize]
    public async Task<IActionResult> Edit(Guid appointmentId, EditAppointment appointment)
    {
        var res= await _service.UpdateAppointmentAsync(appointmentId,appointment);
        return Ok(res);
    }
}
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
    [Route("View")]
    public async Task<IActionResult> View()
    {
        var appointments=await _service.ViewAppointments();
        return Ok(appointments);
    }

    [HttpPut("Edit{AppointmentId}")]
    public async Task<IActionResult> Edit(Guid AppointmentId, EditAppointment appointment)
    {
        var res= await _service.UpdateAppointmentAsync(AppointmentId,appointment);
        return Ok(res);
    }
}
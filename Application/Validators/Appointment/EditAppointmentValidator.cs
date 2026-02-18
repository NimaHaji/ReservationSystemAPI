using Application.Features.Appointments.DTOs;
using FluentValidation;

namespace Application.Validators.Appointment;

public class EditAppointmentValidator : AbstractValidator<EditAppointment>
{
    public EditAppointmentValidator()
    {
        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("زمان شروع الزامی است.")
            .Must(BeValidDate).WithMessage("زمان شروع معتبر نیست.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("زمان پایان الزامی است.")
            .Must(BeValidDate).WithMessage("زمان پایان معتبر نیست.");

        RuleFor(x => x)
            .Must(x => x.EndTime > x.StartTime)
            .WithMessage("زمان پایان باید بعد از زمان شروع باشد.");

        RuleFor(x => x.ServiceIds)
            .NotNull().WithMessage("حداقل یک سرویس باید انتخاب شود.")
            .Must(x => x.Any()).WithMessage("حداقل یک سرویس باید انتخاب شود.")
            .ForEach(id =>
                id.NotEmpty().WithMessage("شناسه سرویس معتبر نیست."));
    }

    private bool BeValidDate(DateTime date)
        => date != default;
}
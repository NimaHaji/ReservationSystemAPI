using Application.Features.Appointments.DTOs;
using FluentValidation;

namespace Application.Validators.Appointment;

public class CreateAppointmentValidator : AbstractValidator<CreateAppointment>
{
    public CreateAppointmentValidator()
    {
        RuleFor(x => x.AppoinmentTitle)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("عنوان نوبت الزامی است.")
            .MinimumLength(3).WithMessage("عنوان نوبت باید حداقل ۳ کاراکتر باشد.")
            .MaximumLength(150).WithMessage("عنوان نوبت نمی‌تواند بیشتر از ۱۵۰ کاراکتر باشد.");

        RuleFor(x => x.ServiceId)
            .NotNull().WithMessage("حداقل یک سرویس باید انتخاب شود.")
            .Must(x => x.Any()).WithMessage("حداقل یک سرویس باید انتخاب شود.")
            .ForEach(id =>
                id.NotEmpty().WithMessage("شناسه سرویس معتبر نیست."));

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("زمان شروع الزامی است.")
            .Must(BeValidDate).WithMessage("زمان شروع معتبر نیست.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("زمان پایان الزامی است.")
            .Must(BeValidDate).WithMessage("زمان پایان معتبر نیست.");

        RuleFor(x => x)
            .Must(x => x.EndTime > x.StartTime)
            .WithMessage("زمان پایان باید بعد از زمان شروع باشد.");
    }

    private bool BeValidDate(DateTime date)
        => date != default;
}
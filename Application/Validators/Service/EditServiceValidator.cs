using Application.Features.Service.DTOs;
using FluentValidation;

namespace Application.Validators.Service;

public class EditServiceValidator : AbstractValidator<EditService>
{
    public EditServiceValidator()
    {
        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("عنوان سرویس الزامی است.")
            .MinimumLength(3).WithMessage("عنوان سرویس باید حداقل ۳ کاراکتر باشد.")
            .MaximumLength(100).WithMessage("عنوان سرویس نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد.");

        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).WithMessage("مدت زمان سرویس باید بیشتر از صفر باشد.")
            .LessThanOrEqualTo(480).WithMessage("مدت زمان سرویس نمی‌تواند بیشتر از ۸ ساعت باشد.");
    }
}
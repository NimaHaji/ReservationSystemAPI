using Application.Features.Auth.DTOs;
using FluentValidation;

namespace Application.Validators.User;

public class LoginUserValidator:AbstractValidator<LoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("ایمیل نا معتبر است .");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("پسوورد الزامی است .");
    }
}
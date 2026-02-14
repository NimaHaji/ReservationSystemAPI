using Application.Features.Auth.DTOs;
using FluentValidation;

namespace Application.Validators.User;

public class LoginUserValidator:AbstractValidator<LoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}
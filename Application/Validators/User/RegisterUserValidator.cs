using Application.Features.Auth.DTOs;
using FluentValidation;

namespace Application.Validators.User;

public class RegisterUserValidator : AbstractValidator<RegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Full name is required")
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithMessage("Full name cannot be empty or whitespace")
            .MaximumLength(50)
            .WithMessage("Full name must not exceed 50 characters");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format is invalid")
            .MaximumLength(100);

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters")
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one digit");

        RuleFor(x => x.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^09\d{9}$")
            .WithMessage("Phone number must be a valid mobile number");
    }
}
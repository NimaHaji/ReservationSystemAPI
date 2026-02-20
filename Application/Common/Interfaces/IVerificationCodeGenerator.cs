namespace Application.Common.Interfaces;

public interface IVerificationCodeGenerator
{
    string Generate6DigitCode();
}
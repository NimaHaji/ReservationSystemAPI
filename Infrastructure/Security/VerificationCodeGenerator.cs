using System.Security.Cryptography;
using Application.Common.Interfaces;

namespace Infrastructure.Security;

public class VerificationCodeGenerator:IVerificationCodeGenerator
{
    public string Generate6DigitCode()
    {
        var code = RandomNumberGenerator.GetInt32(100000, 1_000_000);
        return code.ToString();
    }
}
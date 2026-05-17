using System.Text.RegularExpressions;

namespace Domain.ValueObject;

public class PhoneNumber
{
    public string Value { get; private set; }
    private PhoneNumber()
    {
    }

    public PhoneNumber(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));
        
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be empty or whitespace only string.", nameof(value));
        Value = value;
    }
    private static bool IsValidPhoneNumber(string number)
    {
        var pattern = @"^09[0-9]{9}$";
        return Regex.IsMatch(number, pattern);
    }
}
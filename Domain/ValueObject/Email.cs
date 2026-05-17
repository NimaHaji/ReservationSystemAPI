using System.Text.RegularExpressions;

namespace Domain.ValueObject;

public class Email
{
    public string Value { get; private set; }

    private Email()
    {
    }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.");

        if (!IsValidEmail(value))
            throw new ArgumentException("Value must be a valid email address.");
        
        Value = value.ToLowerInvariant();
    }

    private static bool IsValidEmail(string email)
    {
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Email other) return false;
        return Value == other.Value;
    }
    public override string ToString() => Value;
}
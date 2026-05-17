using System.Text.RegularExpressions;
using Domain.Entities;

namespace Domain.ValueObject;

public class Password
{
    public string Hash { get; private set; }

    private Password()
    {
        
    }
    public Password(string hash)
    {
        if(hash == null)
            throw new ArgumentNullException(nameof(hash));
        
        Hash = hash;
    }
    public static Password FromPlainText(string plainText, IHasher hasher)
    {
        if (!IsValidPlainText(plainText)) 
            throw new ArgumentException("Invalid password format");
        return new Password(hasher.Hash(plainText));
    }
    public static bool IsValidPlainText(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            return false;
        
        var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        return Regex.IsMatch(plainText, pattern);
    }
    public bool Verify(string plainText, IHasher hasher)
        => hasher.Verify(Hash, plainText);
}
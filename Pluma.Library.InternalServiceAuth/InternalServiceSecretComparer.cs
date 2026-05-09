using System.Security.Cryptography;
using System.Text;

namespace Pluma.Library.InternalServiceAuth;

public static class InternalServiceSecretComparer
{
    public static bool EqualsUtf8(string? a, string? b)
    {
        if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
            return false;

        var bytesA = Encoding.UTF8.GetBytes(a);
        var bytesB = Encoding.UTF8.GetBytes(b);

        if (bytesA.Length != bytesB.Length)
            return false;

        return CryptographicOperations.FixedTimeEquals(bytesA, bytesB);
    }
}

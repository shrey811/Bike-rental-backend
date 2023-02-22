using System.Security.Cryptography;
using System.Text;

namespace BCP.Infrastructure.Helper;

public class PasswordHelper
{
    public static string HashPassword(string password, string email)
    {
        const int keySize = 64;
        const int iterations = 350000;
        var salt =  Encoding.UTF8.GetBytes(email);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA512,
            keySize);
        return Convert.ToHexString(hash);
    }

    public static bool VerifyPassword(string password, string hash, string email)
    {
        const int keySize = 64;
        const int iterations = 350000;
        var salt = Encoding.UTF8.GetBytes(email);
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm:HashAlgorithmName.SHA512,keySize);
        return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
    }
}
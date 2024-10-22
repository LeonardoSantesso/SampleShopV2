using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers;

public static class PasswordHelper
{
    public static string ComputeHash(string input, HashAlgorithm algorithm)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

        var builder = new StringBuilder();

        foreach (var item in hashedBytes)
        {
            builder.Append(item.ToString("x2"));
        }
        return builder.ToString();
    }

    public static string ComputeHash(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashedBytes = SHA256.Create().ComputeHash(inputBytes);

        var builder = new StringBuilder();

        foreach (var item in hashedBytes)
        {
            builder.Append(item.ToString("x2"));
        }
        return builder.ToString();
    }
}
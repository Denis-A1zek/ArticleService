using System.Security.Cryptography;
using ArticleService.Application.Interfaces.Services.Auth;

namespace ArticleService.Application.Services.Auth;

public sealed class KeyGenerator: IKeyGenerator
{
    private const int KEY_LENGTH = 64;
    private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    public string Generate()
    {
       
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] data = new byte[KEY_LENGTH];
            rng.GetBytes(data);
            return new string(data.Select(b => CHARS[b % CHARS.Length]).ToArray());
        }
    }
}

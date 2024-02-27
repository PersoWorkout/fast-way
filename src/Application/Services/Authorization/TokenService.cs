using System.Security.Cryptography;

namespace Application.Services.Authorization
{
    public static class TokenService
    {
        public static string Generate()
        {
            byte[] tokenData = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenData);
            }

            return BitConverter.ToString(tokenData).Replace("-", "").ToLower();
        }
    }
}

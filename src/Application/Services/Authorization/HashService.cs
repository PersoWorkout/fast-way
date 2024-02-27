namespace Application.Services.Authorization
{
    public static class HashService
    {
        public static string? Hash(string password)
        {
            if (password is null) return null;
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool Verify(string value, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(value, hash);
        }
    }
}

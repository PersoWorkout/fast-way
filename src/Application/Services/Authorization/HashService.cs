﻿namespace Application.Services.Authorization
{
    public class HashService
    {
        public string? Hash(string password)
        {
            if (password is null) return null;
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool Verify(string value, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(value, hash);
        }
    }
}

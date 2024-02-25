﻿namespace Application.Interfaces
{
    public interface IPasswordHasherService
    {
        string Hash(string password);
        bool Verify(string value, string hash);
    }
}

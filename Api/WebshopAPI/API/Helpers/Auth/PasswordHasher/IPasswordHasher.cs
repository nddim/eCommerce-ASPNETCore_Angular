﻿namespace WebAPI.Helpers.Auth.PasswordHasher
{
    public interface IPasswordHasher
    {
        Task<string> Hash(string password);
        Task<bool> Verify(string passwordHash, string inputPassword);
    }
}

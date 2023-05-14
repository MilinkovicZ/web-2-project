﻿using Microsoft.AspNetCore.Mvc;
using WebShop.DTO;
using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(UserLoginDTO userLoginDTO);
        Task Register(UserRegisterDTO userRegisterDTO);
        void Logout();
    }
}

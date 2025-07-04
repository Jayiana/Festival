﻿using ShowTime.DataAccess.GenericRepository;
using ShowTime_BusinessLogic.Dtos.Authentication.Login;
using ShowTime_BusinessLogic.Dtos.Authentication.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime_BusinessLogic.Services
{
    public interface IUserService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace InvestQ.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserExists(string username);
        Task<UserUpdateDto> GetUserByUsernameAsync(string username);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> CreateUserAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateUser(UserUpdateDto userUpdateDto);
    }
}
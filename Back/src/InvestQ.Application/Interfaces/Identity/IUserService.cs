using System.Threading.Tasks;
using InvestQ.Application.Dtos.Identity;
using Microsoft.AspNetCore.Identity;

namespace InvestQ.Application.Interfaces.Identity
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
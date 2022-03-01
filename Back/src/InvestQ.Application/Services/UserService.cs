using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos;
using InvestQ.Application.Interfaces;
using InvestQ.Data.Interfaces;
using InvestQ.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public UserService(UserManager<User> userManager,
                            SignInManager<User> signInManager,
                            IMapper mapper,
                            IUserRepo userRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userRepo = userRepo;
        }
        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users
                                    .SingleOrDefaultAsync(u => u.UserName == userUpdateDto.Username.ToLower());
                
                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception ex)
            {                
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
             try
            {
                var user = _mapper.Map<User>(userDto);

                var result = await _userManager.CreateAsync(user, userDto.PassWord);

                if  (result.Succeeded) 
                {
                    var userToReturn = _mapper.Map<UserDto>(user);

                    return userToReturn;
                }

                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception($"Erro ao tentar criar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUsernameAsync(string username)
        {
             try
            {
                var user = await _userRepo.GetUserByUsernameAsync(username);

                if (user == null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);

                return userUpdateDto;
            }
            catch (Exception ex)
            {                
                throw new Exception($"Erro ao tentar pegar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateUser(UserUpdateDto userUpdateDto)
        {
             try
            {
                var user = await _userRepo.GetUserByUsernameAsync(userUpdateDto.Username);

                if (user == null) return null;

                _mapper.Map(userUpdateDto, user);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                _userRepo.Atualizar<User>(user);

                if (await _userRepo.SalvarMudancasAsync())
                {
                    var userRetorno = await _userRepo.GetUserByUsernameAsync(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }

                return null;

            }
            catch (Exception ex)
            {                
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string username)
        {
             try
            {
                return await _userManager.Users.AnyAsync(user => user.UserName == username.ToLower());
            }
            catch (Exception ex)
            {                
                throw new Exception($"Erro ao tentar verificar se o usuário existe. Erro: {ex.Message}");
            }
        }
    }
}
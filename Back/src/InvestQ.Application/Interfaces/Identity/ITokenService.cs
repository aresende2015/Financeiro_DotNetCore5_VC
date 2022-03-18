using System.Threading.Tasks;
using InvestQ.Application.Dtos.Identity;

namespace InvestQ.Application.Interfaces.Identity
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}
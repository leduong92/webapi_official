using Core.DTO.BaseDto;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserService
    {
        string GetMyName();
        string CreateToken(User user);

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        RefreshToken GenerateRefreshToken();
    }
}

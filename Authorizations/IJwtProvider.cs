using System.Security.Claims;

namespace LinkBox.Authorizations
{
    public interface IJwtProvider 
    {
        Task<string> GenerateToken(UserModel username);

        Task<ClaimsPrincipal> ReadToken(string token);
    }
}

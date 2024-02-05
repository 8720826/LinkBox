using System.Security.Claims;

namespace LinkBox.Authorizations
{
    public interface IJwtProvider 
    {
        Task<ClaimsPrincipal> ReadToken(string token);
    }
}

using Microsoft.AspNetCore.Authorization;

namespace LinkBox.Authorizations
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public UserAuthorizeAttribute()
        {
            AuthenticationSchemes = UserAuthenticationHandler.CustomerSchemeName;
        }
    }
}

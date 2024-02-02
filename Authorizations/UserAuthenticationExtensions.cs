namespace LinkBox.Authorizations
{
    public static class UserAuthenticationExtensions
    {
        public static IServiceCollection AddUserAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(UserAuthenticationHandler.CustomerSchemeName)
          .AddCookie(UserAuthenticationHandler.CustomerSchemeName, options =>
          {
              //登入地址
              options.LoginPath = "/user/login/";
              //登出地址
              options.LogoutPath = "/user/logout/";
              //设置cookie过期时长
              options.ExpireTimeSpan = TimeSpan.FromDays(30);
          });


            return services;
        }

    }
}

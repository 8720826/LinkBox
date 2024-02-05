using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkBox.Authorizations
{
    public class JwtProvider : IJwtProvider
    {
        private readonly string securityKey = "";

        public JwtProvider(IConfiguration configuration)
        {
            securityKey = configuration.GetSection("Jwt").GetValue<string>("securityKey");
        }

        public async Task<string> GenerateToken(UserModel user)
        {
            return await Task.Run(() =>
            {
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

                var securityKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(securityKey));
                var claims = new[]
                {
                    new Claim("Id",Guid.NewGuid().ToString()),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeyBase64));
                var token = new JwtSecurityToken(
                    issuer: "",                    // 发布者
                    audience: "",                // 接收者
                    notBefore: DateTime.Now,                                                          // token签发时间
                    expires: DateTime.Now.AddDays(30),                                                // token过期时间
                    claims: claims,                                                                   // 该token内存储的自定义字段信息
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)    // 用于签发token的秘钥算法
                );
                return jwtSecurityTokenHandler.WriteToken(token);
            });
        }



        public async Task<ClaimsPrincipal> ReadToken(string token)
        {
            return await Task.Run(() =>
            {
                var securityKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(securityKey));
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeyBase64));

                var jwtHander = new JwtSecurityTokenHandler();
                var claimsIdentity = jwtHander.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "",
                    ValidAudience = "",
                    IssuerSigningKey = key,
                    ValidateLifetime = true,

                }, out _);
                return claimsIdentity;
            });
        }
    }
}

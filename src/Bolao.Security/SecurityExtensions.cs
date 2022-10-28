using Bolao.Security.Database;
using Bolao.Security.Interface;
using Bolao.Security.Repositories;
using Bolao.Security.Services;
using Bolao.Security.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bolao.Security
{
    public static class SecurityExtensions
    {
        public static IServiceCollection AddBolaoSecurity(this IServiceCollection service, IConfiguration configuration)
        {
            var tokenConfig = configuration.GetSection(nameof(TokenConfiguration)).Get<TokenConfiguration>();
            var key = Encoding.ASCII.GetBytes(tokenConfig.Key);
            service.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            service.AddScoped<ITokenService, TokenService>();
            return service;
        }

        public static IServiceCollection AddBolaoSecurityDataBase(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<UsuarioDataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            service.AddScoped<IUsuarioRepository, UsuarioRepository>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            return service;
        }
    }
}

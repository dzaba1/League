using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Dzaba.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Dzaba.AspNet.Jwt
{
    public static class Bootstrapper
    {
        public static void RegisterJwtAuth(this IServiceCollection container)
        {
            Require.NotNull(container, nameof(container));

            container.AddTransient<IJwtOptionsFactory, JwtOptionsFactory>();
            container.AddTransient<ITokenGenerator, TokenGenerator>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            container.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    var provider = container.BuildServiceProvider();
                    var options = provider.GetRequiredService<IJwtOptionsFactory>().Create();

                    cfg.RequireHttpsMetadata = options.RequireHttpsMetadata;
                    cfg.SaveToken = options.SaveToken;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = options.ValidIssuer,
                        ValidAudience = options.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
        }
    }
}

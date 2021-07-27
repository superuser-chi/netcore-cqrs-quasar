using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Users;
using Domain.Configuration;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Core.Security
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppConfig _appConfig;

        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger, IOptions<AppConfig> appConfig)
        {
            _logger = logger;
            _next = next;
            _appConfig = appConfig.Value;
        }

        public async Task Invoke(HttpContext context, IMediator mediator)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!String.IsNullOrEmpty(authHeader))
            {
                String token = authHeader.Split(" ").Last();
                await attachUserToContextAsync(context, mediator, token);
            }

            await _next(context);
        }

        private async Task attachUserToContextAsync(HttpContext context, IMediator mediator, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appConfig.Secret);
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                _logger.LogDebug($"TOKEN: {token}");
                var claims = tokenHandler.ValidateToken(token, validations, out var tokenSecure);
                var userId = claims.FindFirst(x => x.Type == "Id").Value;
                _logger.LogDebug($"CLAIMS: {claims}");


                var user = await mediator.Send(new Details.Query { Id = userId });

                // attach user to context on successful jwt validation
                context.Items["User"] = user;

            }
            catch
            {
                // what to 
                // throw e;
                _logger.LogWarning($"Could not decode token: {token}");
            }
        }
    }
}

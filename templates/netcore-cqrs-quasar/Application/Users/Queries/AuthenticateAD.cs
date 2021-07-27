namespace Application.Users.Queries
{
    using Persistence.Identity;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using System.Threading;
    using System;
    using Domain.Configuration;
    using global::Application.Users.Commons;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Logging;

    namespace Application.Users
    {
        public class AuthenticateAD
        {
            public class Query : IRequest<ApplicationUser>
            {
                public string Username { get; set; }
                public string Password { get; set; }
            }

            public class Handler : IRequestHandler<Query, ApplicationUser>
            {
                private readonly UserManager<ApplicationUser> _userManager;
                private readonly SignInManager<ApplicationUser> _signInManager;
                private readonly AppConfig _appConfig;
                private readonly ILogger<AuthenticateAD> _logger;
                public Handler(UserManager<ApplicationUser> userManager, ILogger<AuthenticateAD> logger, SignInManager<ApplicationUser> signInManager, IOptions<AppConfig> options)
                {
                    _logger = logger;
                    _appConfig = options.Value;
                    _signInManager = signInManager;
                    _userManager = userManager;
                }

                public async Task<ApplicationUser> Handle(Query query, CancellationToken cancellationToken)
                {
                    try
                    {

                        if (ADHelper.ValidateAgainstAD(_appConfig, query.Username, query.Username))
                        {
                            var loginRes = await _signInManager.PasswordSignInAsync(query.Username, query.Password, false, lockoutOnFailure: false);
                            if (!loginRes.Succeeded)
                            {
                                var result = await _userManager.ResetPassword(query.Username, query.Password);
                                if (!result.Succeeded)
                                {
                                    throw new InvalidOperationException();
                                }
                            }
                        }
                        return await _userManager.Authenticate<AuthenticateAD>(_logger, _signInManager, _appConfig, query.Username, query.Password);

                    }
                    catch (Exception)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
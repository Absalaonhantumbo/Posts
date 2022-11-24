using System.Net;
using Aplication.Dtos;
using Aplication.Errors;
using Aplication.Interfaces;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplication.Accounts;

public class Login
{
    public class LoginCommand: IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    public class LoginHandler: IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public LoginHandler(SignInManager<User> signInManager, 
            UserManager<User> userManager, 
            ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                throw new RestException(HttpStatusCode.Unauthorized);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded) throw new RestException(HttpStatusCode.Unauthorized);
            
            return new LoginResponse
            {
                Email = user.Email,
                Fullname = user.Fullname,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
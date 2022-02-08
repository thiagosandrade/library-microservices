using Library.Auth.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Auth.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;

        public AuthService(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }

        public async Task<object> Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var result = await _mediator.Send(new GetAllUserWithRolesQuery());

            var user = result.Users.Where(x => x.Login.Equals(login) && x.Password.Equals(password)).FirstOrDefault();

            if (user == null)
                return null;

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Surname, user.Surname),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in user.UserRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.UserRole));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:Secret"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new { token = $"Bearer { tokenHandler.WriteToken(token)}", expiration = tokenDescriptor.Expires };
        }
    }
}

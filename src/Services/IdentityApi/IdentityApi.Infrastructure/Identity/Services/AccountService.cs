using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityApi.Application.Base;
using IdentityApi.Application.Services.Identity.Contracts;
using IdentityApi.Domain.DomainEvents;
using IdentityApi.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Entities;
using Shared.Logs.Services;
using Shared.Requests;

namespace IdentityApi.Infrastructure.Identity.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public AccountService(ILogServices logServices, IConfiguration configuration,
            RoleManager<AppRole> roleManager,
            UserManager<AppUser> userManager, IMediator mediator) : base(logServices)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<CommandResult<string>> Login(LoginRequest login)
        {
            var user = await _userManager.FindByNameAsync(login.Usuario);

            if (user is null)
            {
                return await CommandResult<string>.FailAsync("Usuário ou senha inválidos");
            }

            if (!user.Ativo)
            {
                return await CommandResult<string>.FailAsync("O seu usuário não está ativo.");
            }

            var result = await _userManager.CheckPasswordAsync(user, login.Senha);

            if (!result)
            {
                return await CommandResult<string>.FailAsync("Usuário ou senha inválidos");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var roleClaims = new List<Claim>();
            var permissionClaims = new List<Claim>();

            foreach (var role in userRoles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
                var thisRole = await _roleManager.FindByNameAsync(role);
                var roleClaimsFromDb = await _roleManager.GetClaimsAsync(thisRole);
                permissionClaims.AddRange(roleClaimsFromDb);
            }

            var claims = new List<Claim>()
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.UserName),
                    new(ClaimTypes.Email, user.Email),
                }
                .Union(userClaims)
                .Union(permissionClaims)
                .Union(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );
            return await CommandResult<string>.SuccessAsync(new JwtSecurityTokenHandler().WriteToken(token),
                "Token gerado com sucesso");
        }

        public async Task<ICommandResult> Register(RegisterRequest model)
        {
            var username = $"{model.Nome}.{model.Sobrenome}".ToLower();
            var newUser = new AppUser
                { UserName = username, Email = model.Email, Nome = model.Nome, Sobrenome = model.Sobrenome, Ativo = true};

            var result = await _userManager.CreateAsync(newUser, model.Senha);

            if (result.Succeeded)
            {
                var userCreatedEvent = new UserCreatedDomainEvent(newUser.Id);
                await _mediator.Publish(userCreatedEvent);
                return await CommandResult.SuccessAsync("Registro realizado com sucesso!");
            }

            var errors = result.Errors.Select(x => x.Description).ToList();
            return await CommandResult.FailAsync(errors);
        }
    }
}
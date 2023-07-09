using AutoMapper;
using IdentityApi.Application.Base;
using IdentityApi.Application.Contracts.Services;
using IdentityApi.Application.Services.Identity.Contracts;
using IdentityApi.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Entities;
using Shared.Logs.Services;
using Shared.Requests;
using Shared.Responses;

namespace IdentityApi.Infrastructure.Identity.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<AppUser> userManager,
            IMapper mapper,
            RoleManager<AppRole> roleManager,
            ICurrentUserService currentUserService,
            ILogServices logServices) : base(logServices)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _currentUserService = currentUserService;
        }

        public async Task<CommandResult<List<UserResponse>>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var CommandResult = _mapper.Map<List<UserResponse>>(users);
            return await CommandResult<List<UserResponse>>.SuccessAsync(CommandResult);
        }

        public async Task<CommandResult<UserResponse>> GetAsync(string userId)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var CommandResult = _mapper.Map<UserResponse>(user);
            return await CommandResult<UserResponse>.SuccessAsync(CommandResult);
        }

        public async Task<CommandResult<UserRolesResponse>> GetRolesAsync(string userId)
        {
            var viewModel = new List<UserRoleModel>();
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var role in roles)
            {
                var userRolesViewModel = new UserRoleModel
                {
                    RoleName = role.Name,
                    RoleDescription = role.Description
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }

                viewModel.Add(userRolesViewModel);
            }

            var CommandResult = new UserRolesResponse { UserRoles = viewModel };
            return await CommandResult<UserRolesResponse>.SuccessAsync(CommandResult);
        }

        public async Task<ICommandResult> UpdateRolesAsync(UpdateUserRolesRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user.UserName == "admin")
            {
                return await CommandResult<string>.FailAsync("Não pode editar esse usuário.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var selectedRoles = request.UserRoles.Where(x => x.Selected).ToList();

            var currentUser = await _userManager.FindByIdAsync(_currentUserService.UserId);
            if (!await _userManager.IsInRoleAsync(currentUser, Roles.Admin))
            {
                var tryToAddAdministratorRole = selectedRoles
                    .Any(x => x.RoleName == Roles.Admin);
                var userHasAdministratorRole = roles.Any(x => x == Roles.Admin);
                if (tryToAddAdministratorRole && !userHasAdministratorRole ||
                    !tryToAddAdministratorRole && userHasAdministratorRole)
                {
                    return await CommandResult<string>.FailAsync(
                        "Não é permitido adicionar/editar role admin se você não é um.");
                }
            }

            var CommandResult = await _userManager.RemoveFromRolesAsync(user, roles);
            CommandResult = await _userManager.AddToRolesAsync(user, selectedRoles.Select(y => y.RoleName));
            return await CommandResult<string>.SuccessAsync("Roles atualizadas");
        }

        public async Task<ICommandResult> UpdateUserStatusAsync(UpdateUserStatusRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.IdUsuario);
            user.Ativo = request.AtivarUsuario;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return await CommandResult.SuccessAsync("Status do usuário atualizado com sucesso");
            }
            else
            {
                return await CommandResult.FailAsync(result.Errors.Select(x => x.Description).ToList());
            }
        }

        public async Task<ICommandResult> UpdateUserAsync(UserRequest request)
        {
            var isAdmin = _currentUserService.Claims.IsInRole(Roles.Admin);
            var isEditedUser = request.Id == _currentUserService.UserId;
            if (!isAdmin && !isEditedUser)
            {
                return await CommandResult.FailAsync("Só é possivel editar o próprio usuário ou sendo admin.");
            }

            var user = await _userManager.FindByIdAsync(request.Id);
            user.Nome = request.Nome;
            user.Sobrenome = request.Sobrenome;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return await CommandResult.SuccessAsync("Usuário atualizado com sucesso");
            }
            else
            {
                return await CommandResult.FailAsync(result.Errors.Select(x => x.Description).ToList());
            }
        }
    }
}
using AuthenticationApi.Application.Base;
using AuthenticationApi.Application.Contracts.Services;
using AuthenticationApi.Application.Services.Identity.Contracts;
using AuthenticationApi.Domain.Entites;
using AuthenticationApi.Infrastructure.Identity.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Entities;
using Shared.Logs.Services;
using Shared.Requests;
using Shared.Responses;

namespace AuthenticationApi.Infrastructure.Identity.Services;

public class RoleService : BaseService, IRoleService
{
    private readonly IRoleClaimService _roleClaimService;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public RoleService(ILogServices logServices, IRoleClaimService roleClaimService, RoleManager<AppRole> roleManager,
        UserManager<AppUser> userManager, IMapper mapper, ICurrentUserService currentUserService)
        : base(logServices)
    {
        _roleClaimService = roleClaimService;
        _roleManager = roleManager;
        _userManager = userManager;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<CommandResult<string>> DeleteAsync(string id)
    {
        var existingRole = await _roleManager.FindByIdAsync(id);
        if (existingRole?.Name != Roles.User && existingRole?.Name != Roles.Admin)
        {
            bool roleIsNotUsed = true;
            var allUsers = await _userManager.Users.ToListAsync();
            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, existingRole.Name))
                {
                    roleIsNotUsed = false;
                    break;
                }
            }

            if (roleIsNotUsed)
            {
                await _roleManager.DeleteAsync(existingRole);
                return await CommandResult<string>.SuccessAsync($"Role {existingRole.Name} deletada!");
            }
            else
            {
                return await CommandResult<string>.FailAsync(
                    $"Role {existingRole.Name} não pode ser deletada pois está em uso!");
            }
        }
        else
        {
            return await CommandResult<string>.FailAsync($"Role {existingRole.Name} não pode ser deletada!");
        }
    }

    public async Task<CommandResult<List<RoleResponse>>> GetAllAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        var rolesResponse = _mapper.Map<List<RoleResponse>>(roles);
        return await CommandResult<List<RoleResponse>>.SuccessAsync(rolesResponse);
    }

    public async Task<CommandResult<PermissionResponse>> GetAllPermissionsAsync(string roleId)
    {
        var model = new PermissionResponse();
        var allPermissions = GetAllPermissions();
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role != null)
        {
            model.RoleId = role.Id;
            model.RoleName = role.Name;
            var roleClaimsResult = await _roleClaimService.GetAllByRoleIdAsync(role.Id);
            if (roleClaimsResult.Succeeded)
            {
                var roleClaims = roleClaimsResult.Data;
                var allClaimValues = allPermissions.Select(x => x.Value).ToList();
                var roleClaimValues = roleClaims.Select(x => x.Value).ToList();
                var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
                foreach (var permission in allPermissions)
                {
                    if (authorizedClaims.Any(a => a == permission.Value))
                    {
                        permission.Selected = true;

                        var roleClaim = roleClaims.SingleOrDefault(a => a.Value == permission.Value);
                        if (roleClaim?.Description != null)
                        {
                            permission.Description = roleClaim.Description;
                        }

                        if (roleClaim?.Group != null)
                        {
                            permission.Group = roleClaim.Group;
                        }
                    }
                }
            }
            else
            {
                return await CommandResult<PermissionResponse>.FailAsync(roleClaimsResult.Messages);
            }
        }

        model.RoleClaims = allPermissions;
        return await CommandResult<PermissionResponse>.SuccessAsync(model);
    }

    public async Task<CommandResult<string>> SaveAsync(RoleRequest request)
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            var existingRole = await _roleManager.FindByNameAsync(request.Name);
            if (existingRole != null) return await CommandResult<string>.FailAsync("Role similar já existe");
            var response = await _roleManager.CreateAsync(new AppRole(request.Name, request.Description));
            if (response.Succeeded)
            {
                return await CommandResult<string>.SuccessAsync("Role criada!");
            }
            else
            {
                return await CommandResult<string>.FailAsync(
                    response.Errors.Select(x => x.Description).ToList());
            }
        }
        else
        {
            var existingRole = await _roleManager.FindByIdAsync(request.Id);
            if (existingRole.Name == Roles.Admin || existingRole.Name == Roles.User)
            {
                return await CommandResult<string>.FailAsync("Não é permitido modificar essa role");
            }

            existingRole.Name = request.Name;
            existingRole.NormalizedName = request.Name.ToUpper();
            existingRole.Description = request.Description;
            await _roleManager.UpdateAsync(existingRole);
            return await CommandResult<string>.SuccessAsync("Role atualizada!");
        }
    }

    public async Task<CommandResult<string>> UpdatePermissionsAsync(PermissionRequest request)
    {
        try
        {
            var errors = new List<string>();
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            if (role.Name == Roles.Admin)
            {
                var currentUser = await _userManager.Users.SingleAsync(x => x.Id == _currentUserService.UserId);
                if (await _userManager.IsInRoleAsync(currentUser, Roles.Admin))
                {
                    return await CommandResult<string>.FailAsync("Não é permitido modificar permissões para essa role");
                }
            }

            var selectedClaims = request.RoleClaims.Where(a => a.Selected).ToList();
            if (role.Name == Roles.Admin)
            {
                if (!selectedClaims.Any(x => x.Value == Permissions.Roles.View)
                    || !selectedClaims.Any(x => x.Value == Permissions.RoleClaims.View)
                    || !selectedClaims.Any(x => x.Value == Permissions.RoleClaims.Edit))
                {
                    return await CommandResult<string>.FailAsync(string.Format(
                        "Não é permitido deselecionar {0} ou {1} ou {2} para essa Role.",
                        Permissions.Roles.View, Permissions.RoleClaims.View, Permissions.RoleClaims.Edit));
                }
            }

            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            foreach (var claim in selectedClaims)
            {
                var addResult = await _roleManager.AddPermissionClaim(role, claim.Value);
                if (!addResult.Succeeded)
                {
                    errors.AddRange(Enumerable.Select<IdentityError, string>(addResult.Errors, e => e.Description));
                }
            }

            var addedClaims = await _roleClaimService.GetAllByRoleIdAsync(role.Id);
            if (addedClaims.Succeeded)
            {
                foreach (var claim in selectedClaims)
                {
                    var addedClaim =
                        addedClaims.Data.SingleOrDefault(x => x.Type == claim.Type && x.Value == claim.Value);
                    if (addedClaim != null)
                    {
                        claim.Id = addedClaim.Id;
                        claim.RoleId = addedClaim.RoleId;
                        var saveResult = await _roleClaimService.SaveAsync(claim);
                        if (!saveResult.Succeeded)
                        {
                            errors.AddRange(saveResult.Messages);
                        }
                    }
                }
            }
            else
            {
                errors.AddRange(addedClaims.Messages);
            }

            if (errors.Any())
            {
                return await CommandResult<string>.FailAsync(errors);
            }

            return await CommandResult<string>.SuccessAsync("Permissões atualizadas");
        }
        catch (Exception ex)
        {
            return await CommandResult<string>.FailAsync(ex.Message);
        }
    }
    private List<RoleClaimResponse> GetAllPermissions()
    {
        var allPermissions = new List<RoleClaimResponse>();

        #region GetPermissions

        allPermissions.GetAllPermissions();

        #endregion GetPermissions

        return allPermissions;
    }
}
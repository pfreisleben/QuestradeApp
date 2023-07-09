using AuthenticationApi.Application.Base;
using AuthenticationApi.Application.Contracts.Services;
using AuthenticationApi.Application.Services.Identity.Contracts;
using AuthenticationApi.Domain.Entites;
using AuthenticationApi.Infrastructure.Persistence.AppDb;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Logs.Services;
using Shared.Requests;
using Shared.Responses;

namespace AuthenticationApi.Infrastructure.Identity.Services;

public class RoleClaimService : BaseService, IRoleClaimService
{
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ApplicationDbContext _db;

    public RoleClaimService(ILogServices logServices,
        IMapper mapper, ICurrentUserService currentUserService, ApplicationDbContext db) : base(logServices)
    {
        _mapper = mapper;
        _currentUserService = currentUserService;
        _db = db;
    }

    public async Task<CommandResult<string>> DeleteAsync(int id)
    {
        var existingRoleClaim = await _db.RoleClaims.FirstOrDefaultAsync(x => x.Id == id);
        if (existingRoleClaim != null)
        {
            _db.RoleClaims.Remove(existingRoleClaim);
            await _db.SaveChangesAsync();
            return await CommandResult<string>.SuccessAsync("Role Claim deletada");
        }
        else
        {
            return await CommandResult<string>.FailAsync("Role Claim não existe");
        }
    }

    public async Task<CommandResult<List<RoleClaimResponse>>> GetAllAsync()
    {
        var roleClaims = await _db.RoleClaims.AsNoTracking().ToListAsync();
        var roleClaimsResponse = _mapper.Map<List<RoleClaimResponse>>(roleClaims);
        return await CommandResult<List<RoleClaimResponse>>.SuccessAsync(roleClaimsResponse);
    }

    public async Task<CommandResult<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId)
    {
        var roleClaims = await _db.RoleClaims.AsNoTracking()
            .Where(x => x.RoleId == roleId).ToListAsync();

        var roleClaimsResponse = _mapper.Map<List<RoleClaimResponse>>(roleClaims);
        return await CommandResult<List<RoleClaimResponse>>.SuccessAsync(roleClaimsResponse);
    }

    public async Task<CommandResult<RoleClaimResponse>> GetByIdAsync(int id)
    {
        var roleClaim = await _db.RoleClaims
            .SingleOrDefaultAsync(x => x.Id == id);
        var roleClaimResponse = _mapper.Map<RoleClaimResponse>(roleClaim);
        return await CommandResult<RoleClaimResponse>.SuccessAsync(roleClaimResponse);
    }

    public async Task<CommandResult<string>> SaveAsync(RoleClaimRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RoleId))
        {
            return await CommandResult<string>.FailAsync("É obrigatório informar a role");
        }

        if (request.Id == 0)
        {
            var existingRoleClaim =
                await _db.RoleClaims
                    .SingleOrDefaultAsync(x =>
                        x.RoleId == request.RoleId && x.ClaimType == request.Type && x.ClaimValue == request.Value);
            if (existingRoleClaim != null)
            {
                return await CommandResult<string>.FailAsync("Uma role claim similar já existe!");
            }

            var roleClaim = _mapper.Map<AppRoleClaim>(request);
            await _db.RoleClaims.AddAsync(roleClaim);
            await _db.SaveChangesAsync();
            return await CommandResult<string>.SuccessAsync("Role claim criada");
        }
        else
        {
            var existingRoleClaim =
                await _db.RoleClaims
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
            if (existingRoleClaim == null)
            {
                return await CommandResult<string>.SuccessAsync("Role claim não existe");
            }
            else
            {
                existingRoleClaim.ClaimType = request.Type;
                existingRoleClaim.ClaimValue = request.Value;
                existingRoleClaim.RoleId = request.RoleId;
                _db.RoleClaims.Update(existingRoleClaim);
                await _db.SaveChangesAsync();
                return await CommandResult<string>.SuccessAsync("Role claim atualizada com sucesso!");
            }
        }
    }
}
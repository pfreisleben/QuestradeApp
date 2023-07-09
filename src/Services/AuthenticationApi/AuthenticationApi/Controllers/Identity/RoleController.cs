using AuthenticationApi.Application.Services.Identity.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Entities;
using Shared.Logs.Services;
using Shared.Requests;

namespace AuthenticationApi.Controllers.Identity
{
    [Route("api/identity/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [Authorize(Policy = Permissions.Roles.View)]
        [HttpGet]
        public async Task<ActionResult<ICommandResult>> GetAll()
        {
            var retorno = await _roleService.GetAllAsync();
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }


        [Authorize(Policy = Permissions.RoleClaims.View)]
        [HttpGet("permissions/{roleId}")]
        public async Task<ActionResult<ICommandResult>> GetPermissionsByRoleId([FromRoute] string roleId)
        {
            var retorno = await _roleService.GetAllPermissionsAsync(roleId);
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }


        [Authorize(Policy = Permissions.Roles.Create)]
        [HttpPost]
        public async Task<ActionResult<ICommandResult>> Post(RoleRequest request)
        {
            var retorno = await _roleService.SaveAsync(request);
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }

        [Authorize(Policy = Permissions.Roles.Delete)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ICommandResult>> Delete(string id)
        {
            var retorno = await _roleService.DeleteAsync(id);
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }


        [Authorize(Policy = Permissions.RoleClaims.Edit)]
        [HttpPut("permissions/update")]
        public async Task<ActionResult<ICommandResult>> Update([FromBody] PermissionRequest model)
        {
            var retorno = await _roleService.UpdatePermissionsAsync(model);
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }
    }
}
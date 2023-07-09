using AuthenticationApi.Application.Services.Identity.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Requests;

namespace AuthenticationApi.Controllers.Identity
{
    [Authorize]
    [Route("api/identity/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Get Users Details
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var retorno = await _userService.GetAllAsync();
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var retorno = await _userService.GetAsync(id);
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }

        /// <summary>
        /// Get User Roles By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet("roles/{id}")]
        public async Task<IActionResult> GetRolesAsync(string id)
        {
            var retorno = await _userService.GetRolesAsync(id);
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }

        /// <summary>
        /// Update Roles for User
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Users.Edit)]
        [HttpPut("roles/{id}")]
        public async Task<IActionResult> UpdateRolesAsync(UpdateUserRolesRequest request)
        {
            var retorno = await _userService.UpdateRolesAsync(request);
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }

        /// <summary>
        /// Atualiza status usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Users.Edit)]
        [HttpPut("updateUserStatusAsync")]
        public async Task<IActionResult> UpdateUserStatusAsync(UpdateUserStatusRequest request)
        {
            var retorno = await _userService.UpdateUserStatusAsync(request);
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }

        /// <summary>
        /// Atualiza  usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize]
        [HttpPut("updateUserAsync")]
        public async Task<IActionResult> UpdateUserAsync(UserRequest request)
        {
            var retorno = await _userService.UpdateUserAsync(request);
            if (retorno.Succeeded)
                return Ok(retorno);
            return BadRequest(retorno);
        }
    }
}
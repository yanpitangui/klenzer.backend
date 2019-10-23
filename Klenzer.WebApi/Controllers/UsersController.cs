using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using Klenzer.Domain.Entities;
using System;
using Klenzer.WebApi.Extensions;
using Microsoft.AspNetCore.Http;
using Klenzer.WebApi.Controllers.Inputs;
using Klenzer.Domain.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Klenzer.WebApi.Controllers.Outputs;

namespace Klenzer.WebApi.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly IUserRepository _userRepository;

        public UsersController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginInput userParam)
        {
            var user = await _userRepository.Authenticate(userParam.MapTo<User>());

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(_authService.GenerateToken(user));
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody]CreateUserInput userParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var entity = userParam.MapTo<User>();
                if(UsuarioExists(entity.Username)) return Conflict("The informed username already exists.");
                var user = await _userRepository.Create(entity);
                if (user != null)
                {
                    await _userRepository.Commit();
                    return Ok(user);
                }
                return Conflict("Something went wrong while creating the user.");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var user = _userRepository.GetAll().Select(x=> x.MapTo<UserOutput>()).AsEnumerable();
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var user = _userRepository.Delete(id);
                await _userRepository.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UsuarioExists(string username)
        {
            return _userRepository.GetAll().Any(e => e.Username == username);
        }
    }
}

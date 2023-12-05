using BookStore.DTO;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Login(model);
                if (status == 0)
                    return BadRequest(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [Route("admin/register")]
        public async Task<IActionResult> AdminRegister(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Registeration(model, UserRoles.Admin);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(AdminRegister), model);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("user/register")]
        public async Task<IActionResult> UserRegister(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Registeration(model, UserRoles.User);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(UserRegister), model);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

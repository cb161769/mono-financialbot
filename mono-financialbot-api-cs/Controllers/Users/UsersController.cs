﻿

using mono_financialbot_backend_bussiness_layer_cs.Dtos;
using mono_financialbot_backend_bussiness_layer_cs.Interfaces;

namespace mono_financialbot_api_cs.Controllers.Users
{
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUser _userService;
        public UsersController(IUser userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto dataUser)
        {
            try
            {
                var result = await _userService.Register(dataUser);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception e)
            {

                return StatusCode(500,e);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                var result = await _userService.Login(login);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception e)
            {

                return StatusCode(500,e);
            }
        }
    }
}

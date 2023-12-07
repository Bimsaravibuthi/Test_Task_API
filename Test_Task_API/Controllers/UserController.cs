using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Test_Task_API.BLL;
using Test_Task_API.Helpers;
using Test_Task_API.Models.User;

namespace Test_Task_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserLogic _userLogic;
        public UserController() 
        {
            _userLogic = new();
        }

        [HttpGet("UserLogin/{Username}/{Password}")]
        public IActionResult UserLogin(string Username, string Password)
        {
            var result = _userLogic.UserLogin(Username, Password);
            if(result is not null)
            {   
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return StatusCode((int)result.StatusCode, new {accessToken = JwtAuth((string[]?)result.Content)});
                }
                if(result.Content is null)
                {
                    return StatusCode((int)result.StatusCode, new { result.StatusCode, result.ReasonPhrase });
                }
                return StatusCode((int)result.StatusCode, result);
            }
            return BadRequest("😕 Bad Input.");
        }
        private static string JwtAuth(string[]? user)
        {
            var claims = new List<Claim>
            {
                new("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Username", user[0]),
                new(ClaimTypes.Name, user[1]),              
                new(ClaimTypes.Email,user[2]),
                new(ClaimTypes.Role, user[3])
            };

            var keyBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);

            var key = new SymmetricSecurityKey(keyBytes);

            var signinCridentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    Constants.Issuer,
                    Constants.Audiance,
                    claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(5),
                    signinCridentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("UserRegister")]
        public IActionResult UserRegister([FromBody] Register register)
        {
            var OPState = _userLogic.UserRegister(register.USR_NAME, register.USR_EMAIL, register.USR_PASSWORD,
                register.USR_USERNAME, register.USR_TPN, register.USR_ACTIVESTATUS,
                Enum.GetName(typeof(RoleStatus), register.USR_STATUS));

            if(OPState != null)
            {
                return StatusCode((int)OPState.StatusCode, OPState);
            }
            return BadRequest("😕 Bad Input.");
        }

        [HttpPut("UserUpdate")]
        public IActionResult UserUpdate([FromBody] Update update)
        {
            return BadRequest("😕 Bad Input.");
        }
    }
}
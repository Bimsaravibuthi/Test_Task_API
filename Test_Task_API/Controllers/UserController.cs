using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Test_Task_API.Helpers;
using Test_Task_API.Models.User;
using Test_Task_API.Shared;

namespace Test_Task_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userLogic;
        public UserController(IUserRepository userRepository) 
        {
            _userLogic = userRepository;
        }
        [Authorize]
        [HttpGet("UserView/{Id?}")]        
        public IActionResult UserView(int? Id)
        {
            var result = _userLogic.UserView(Id);
            if(result is not null)
            {
                if(result.status == HttpStatusCode.OK)
                {
                    return StatusCode((int)result.status, result.Content);
                }
                if(result.Content is null)
                {
                    return StatusCode((int)result.status, new { result.status, result.ReasonPhrase });
                }
                return StatusCode((int)result.status, result);
            }
            return BadRequest("😕 Bad Input.");
        }

        [HttpGet("UserLogin/{Username}/{Password}")]
        public IActionResult UserLogin(string Username, string Password)
        {
            var result = _userLogic.UserLogin(Username, Password);
            if(result is not null)
            {   
                if (result.status == HttpStatusCode.OK)
                {
                    return StatusCode((int)result.status, new {accessToken = JwtAuth((string[]?)result.Content)});
                }
                if(result.Content is null)
                {
                    return StatusCode((int)result.status, new { result.status, result.ReasonPhrase });
                }
                return StatusCode((int)result.status, result);
            }
            return BadRequest("😕 Bad Input.");
        }
        private static string JwtAuth(string[]? user)
        {
            var claims = new[]
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Username", user[0]),
                new Claim(ClaimTypes.Name, user[1]),              
                new Claim(ClaimTypes.Email, user[2]),
                new Claim(ClaimTypes.Role, user[3]),
            };

            var keyBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);

            var key = new SymmetricSecurityKey(keyBytes);

            var signinCridentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(                   
                    Constants.Audiance,
                    Constants.Issuer,
                    claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signinCridentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("UserRegister")]
        public IActionResult UserRegister([FromBody] RegisterUser registerUser)
        {
            var OPState = _userLogic.UserRegister(registerUser.USR_NAME, registerUser.USR_EMAIL, registerUser.USR_PASSWORD,
                registerUser.USR_USERNAME, registerUser.USR_TPN, registerUser.USR_ACTIVESTATUS,
                Enum.GetName(typeof(RoleStatus), registerUser.USR_STATUS));

            if(OPState is not null)
            {
                if(OPState.Content is null)
                {
                    return StatusCode((int)OPState.status, new {OPState.status, OPState.ReasonPhrase});
                }
                return StatusCode((int)OPState.status, OPState);
            }
            return BadRequest("😕 Bad Input.");
        }

        [HttpPut("UserUpdate/{Id}")]
        public IActionResult UserUpdate([FromBody] UpdateUser updateUser, [FromRoute] int Id)
        {
            var OPState = _userLogic.UserUpdate(Id, updateUser.USR_NAME, updateUser.USR_EMAIL, updateUser.USR_PASSWORD,
                updateUser.USR_USERNAME, updateUser.USR_TPN, updateUser.USR_ACTIVESTATUS,
                Enum.GetName(typeof(RoleStatus), updateUser.USR_STATUS));

            if(OPState is not null)
            {
                if(OPState.Content is null)
                {
                    return StatusCode((int)OPState.status, new {OPState.status, OPState.ReasonPhrase});
                }
                return StatusCode((int)OPState.status, OPState);
            }
            return BadRequest("😕 Bad Input.");
        }

        //[
        //  {
        //    "op": "replace",
        //    "path": "usR_NAME",
        //    "value": "Hansika"
        //  },
        //  {
        //    "op": "replace",
        //    "path": "usR_USERNAME",
        //    "value": "THARUSHIH"
        //  }
        //]

        [HttpPatch("UserPatch/{Id}")]
        public IActionResult UserPatch([FromBody] JsonPatchDocument update, [FromRoute] int Id)
        {
            var OPState = _userLogic.UserPatch(Id, update);
            if(OPState is not null)
            {
                if(OPState.Content is null)
                {
                    return StatusCode((int)OPState.status, new {OPState.status, OPState.ReasonPhrase});
                }
                return StatusCode((int)OPState.status, OPState);
            }
            return BadRequest("😕 Bad Input.");
        }
    }
}
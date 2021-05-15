using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microservice.IdentityServer.Dtos;
using Microservice.IdentityServer.Models;
using Microservice.Shared.BaseController;
using Microservice.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using static IdentityServer4.IdentityServerConstants;

namespace Microservice.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDto request)
        {
            var user = new ApplicationUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                City = request.City
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }

            return NoContent();
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var userId = User.Claims.FirstOrDefault(s => s.Type.Equals(JwtRegisteredClaimNames.Sub));
            if (userId == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userId.Value);
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(new {Id = user.Id, Username = user.UserName, Email = user.Email, City = user.City});
        }
    }
}

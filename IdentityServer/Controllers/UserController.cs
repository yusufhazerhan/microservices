using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.IdentityServer.Dtos;
using Microservice.IdentityServer.Models;
using Microservice.Shared.BaseController;
using Microservice.Shared.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
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
    }
}

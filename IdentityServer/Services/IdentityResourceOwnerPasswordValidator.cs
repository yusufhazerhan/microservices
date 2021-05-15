using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using Microservice.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);
            if (user == null)
            {
                context.Result.CustomResponse = new Dictionary<string, object>()
                {
                    {"Error",new {Error = "Kullanıcı adı veya şifre hatalı."} }
                };
                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, context.Password);
            if (!passwordCheck)
            {
                context.Result.CustomResponse = new Dictionary<string, object>()
                {
                    {"Error",new {Error = "Şifreniz hatalı."} }
                };
                return;
            }

            context.Result = new GrantValidationResult(user.Id,OidcConstants.AuthenticationMethods.Password);
        }
    }
}

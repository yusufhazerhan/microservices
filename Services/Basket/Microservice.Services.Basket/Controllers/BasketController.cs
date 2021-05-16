using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Services.Basket.Dtos;
using Microservice.Services.Basket.Services;
using Microservice.Shared.BaseController;
using Microservice.Shared.Services;

namespace Microservice.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto request)
        {
            var userId = _sharedIdentityService.GetUserId();
            request.UserId = userId;
            var basket = await _basketService.SaveOrUpdate(request);
            return ReturnResponse<bool>(basket);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _sharedIdentityService.GetUserId();
            var basket = await _basketService.Get(userId);
            return ReturnResponse<BasketDto>(basket);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userId = _sharedIdentityService.GetUserId();
            var basket = await _basketService.Delete(userId);
            return ReturnResponse<bool>(basket);
        }
    }
}

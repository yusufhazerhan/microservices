using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microservice.Services.Discount.Models;
using Microservice.Services.Discount.Services;
using Microservice.Shared.BaseController;
using Microservice.Shared.Services;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : BaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiscountEntity entity)
        {
            return ReturnResponse(await _discountService.Create(entity));
        }

        [HttpPut]
        public async Task<IActionResult> Update(DiscountEntity entity)
        {
            return ReturnResponse(await _discountService.Update(entity));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return ReturnResponse(await _discountService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return ReturnResponse(await _discountService.Get(id));
        }

        [HttpGet, Route("GetByCode/{code}")]
        public async Task<IActionResult> Get(string code)
        {
            var userId = _sharedIdentityService.GetUserId();
            return ReturnResponse(await _discountService.Get(code, userId));
        }
    }
}

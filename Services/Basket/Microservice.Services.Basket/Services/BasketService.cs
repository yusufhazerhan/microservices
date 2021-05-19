using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microservice.Services.Basket.Dtos;
using Microservice.Shared.Dtos;

namespace Microservice.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> Get(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto request);
        Task<Response<bool>> Delete(string userId);
    }
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto request)
        {
            var status = await _redisService.GetDatabase().StringSetAsync(request.UserId, JsonSerializer.Serialize<BasketDto>(request));
            return status ? Response<bool>.Success(HttpStatusCode.NoContent) : Response<bool>.Fail("Basket cant save or update", HttpStatusCode.InternalServerError);
        }

        public async Task<Response<BasketDto>> Get(string userId)
        {
            var basket = await _redisService.GetDatabase().StringGetAsync(userId);
            return string.IsNullOrEmpty(basket) ? Response<BasketDto>.Fail("Basket not found", HttpStatusCode.NotFound) :
                Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(basket), HttpStatusCode.OK);
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var isBasketExist = await _redisService.GetDatabase().StringGetAsync(userId);
            if (isBasketExist.IsNull)
            {
                return Response<bool>.Fail("Basket not found", HttpStatusCode.NotFound);
            }

            var deleteBasket = await _redisService.GetDatabase().KeyDeleteAsync(userId);
            return deleteBasket ? Response<bool>.Success(HttpStatusCode.NoContent) : Response<bool>.Fail("Basket cant delete", HttpStatusCode.InternalServerError);
        }
    }
}

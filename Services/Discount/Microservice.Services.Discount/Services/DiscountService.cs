using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using Microservice.Services.Discount.Models;
using Microservice.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Microservice.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<DiscountEntity>>> Get();
        Task<Response<DiscountEntity>> Get(int id);
        Task<Response<DiscountEntity>> Get(string code, string userId);
        Task<Response<NoContent>> Create(DiscountEntity entity);
        Task<Response<NoContent>> Update(DiscountEntity entity);
        Task<Response<NoContent>> Delete(int id);

    }

    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<List<DiscountEntity>>> Get()
        {
            var discounts = await _dbConnection.QueryAsync<DiscountEntity>("SELECT * FROM discount");
            return Response<List<DiscountEntity>>.Success(discounts.ToList(), HttpStatusCode.OK);
        }

        public async Task<Response<DiscountEntity>> Get(int id)
        {
            var discount = (await _dbConnection.QueryAsync<DiscountEntity>($"SELECT * FROM discount WHERE id=@Id", new { id })).SingleOrDefault();
            if (discount == null)
                return Response<DiscountEntity>.Fail("Discount Not Found", HttpStatusCode.NotFound);

            return Response<DiscountEntity>.Success(discount, HttpStatusCode.OK);
        }

        public async Task<Response<DiscountEntity>> Get(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<DiscountEntity>(
                "SELECT * FROM discount WHERE code=@Code and userId=@UserId", new
                {
                    Code = code,
                    UserId = userId
                })).FirstOrDefault();

            if (discount == null)
                return Response<DiscountEntity>.Fail("Discount Not Found", HttpStatusCode.NotFound);

            return Response<DiscountEntity>.Success(discount, HttpStatusCode.OK);
        }

        public async Task<Response<NoContent>> Create(DiscountEntity entity)
        {
            var saveDiscount = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES(@UserId,@Rate,@Code)", entity);
            return saveDiscount > 0 ? Response<NoContent>.Success(HttpStatusCode.Created) : Response<NoContent>.Fail("Error accrued while adding", HttpStatusCode.InternalServerError);
        }

        public async Task<Response<NoContent>> Update(DiscountEntity entity)
        {
            var discount = await Get(entity.Id);
            if (!discount.IsSuccess)
                return Response<NoContent>.Fail(discount.Errors, discount.StatusCode);

            var updateDiscount = await _dbConnection.ExecuteAsync("UPDATE discount SET userId=@UserId, rate=@Rate, code=@Code WHERE id=@Id", entity);
            return updateDiscount > 0 ? Response<NoContent>.Success(HttpStatusCode.NoContent) : Response<NoContent>.Fail("Error accrued while updating", HttpStatusCode.InternalServerError);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var discount = await Get(id);
            if (!discount.IsSuccess)
                return Response<NoContent>.Fail(discount.Errors, discount.StatusCode);

            var deleteDiscount = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE id=@id", new { id });
            return deleteDiscount > 0 ? Response<NoContent>.Success(HttpStatusCode.NoContent) : Response<NoContent>.Fail("Error accrued while updating", HttpStatusCode.InternalServerError);

        }
    }
}

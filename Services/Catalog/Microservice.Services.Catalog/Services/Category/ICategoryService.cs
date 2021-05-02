using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.Services.Catalog.Dtos.Category;
using Microservice.Services.Catalog.Models;
using Microservice.Shared.Dtos;

namespace Microservice.Services.Catalog.Services.Category
{
    public interface ICategoryService
    {
        Task<Response<CategoryDto>> Create(CategoryEntity category);
        Task<Response<NoContent>> Update(CategoryDto request);
        Task<Response<NoContent>> Delete(string id);
        Task<Response<CategoryDto>> Get(string id);
        Task<Response<List<CategoryDto>>> Get();
    }
}

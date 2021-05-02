using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microservice.Services.Catalog.Dtos.Category;
using Microservice.Services.Catalog.Models;
using Microservice.Services.Catalog.Settings;
using Microservice.Shared.Dtos;
using MongoDB.Driver;

namespace Microservice.Services.Catalog.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<CategoryEntity> _categoryCollection;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<CategoryEntity>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<CategoryDto>> Create(CategoryEntity category)
        {
            await _categoryCollection.InsertOneAsync(category);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 201);
        }
        public async Task<Response<NoContent>> Update(CategoryDto request)
        {
            var category = _mapper.Map<CategoryEntity>(request);
            var result =await _categoryCollection.FindOneAndReplaceAsync(s => s.Id.Equals(category.Id), category);
            return result == null ? Response<NoContent>.Fail("Category not found.", 404) : Response<NoContent>.Success(200);
        }
        public async Task<Response<NoContent>> Delete(string id)
        {
            var result = await _categoryCollection.DeleteOneAsync(s => s.Id.Equals(id));
            return result.DeletedCount <= 0 ? Response<NoContent>.Fail("Category not found", 404) : Response<NoContent>.Success(204);
        }
        public async Task<Response<CategoryDto>> Get(string id)
        {
            var category = await _categoryCollection.Find(s => s.Id.Equals(id)).FirstOrDefaultAsync();
            return category == null ? Response<CategoryDto>.Fail("Category not found.", 404) :
                Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
        public async Task<Response<List<CategoryDto>>> Get()
        {
            var categories = await _categoryCollection.Find(s => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }
    }
}

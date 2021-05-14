using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microservice.Services.Catalog.Dtos.Category;
using Microservice.Services.Catalog.Dtos.Course;
using Microservice.Services.Catalog.Models;
using Microservice.Services.Catalog.Settings;
using Microservice.Shared.Dtos;
using MongoDB.Driver;

namespace Microservice.Services.Catalog.Services.Course
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<CourseEntity> _courseCollection;
        private readonly IMongoCollection<CategoryEntity> _categoryCollection;
        private readonly IMapper _mapper;
        public CourseService(IDatabaseSettings databaseSetting, IMapper mapper)
        {
            var client = new MongoClient(databaseSetting.ConnectionString);
            var database = client.GetDatabase(databaseSetting.DatabaseName);
            _courseCollection = database.GetCollection<CourseEntity>(databaseSetting.CourseCollectionName);
            _categoryCollection = database.GetCollection<CategoryEntity>(databaseSetting.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<CourseDto>> Create(CourseCreateDto request)
        {
            var course = _mapper.Map<CourseEntity>(request);
            await _courseCollection.InsertOneAsync(course);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 201);
        }
        public async Task<Response<NoContent>> Update(CourseUpdateDto request)
        {
            var course = _mapper.Map<CourseEntity>(request);
            var result = await _courseCollection.FindOneAndReplaceAsync(s => s.Id.Equals(course.Id), course);
            return result == null ? Response<NoContent>.Fail("Course not found.", 404) : Response<NoContent>.Success(200);
        }
        public async Task<Response<NoContent>> Delete(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(s => s.Id.Equals(id));
            return result.DeletedCount <= 0 ? Response<NoContent>.Fail("Course not found.", 404) : Response<NoContent>.Success(204);
        }
        public async Task<Response<List<CourseDto>>> Get()
        {
            var courses = await _courseCollection.Find(x => true).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(s => s.Id.Equals(course.CategoryId)).FirstAsync();
                }
            }
            else
            {
                courses = new List<CourseEntity>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
        public async Task<Response<CourseDto>> Get(string id)
        {
            var course = await _courseCollection.Find(s => s.Id.Equals(id)).FirstOrDefaultAsync();

            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found.", 404);
            }

            course.Category = await _categoryCollection.Find(s => s.Id.Equals(course.CategoryId)).FirstAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }
        public async Task<Response<List<CourseDto>>> GetByUserId(string userId)
        {
            var courses = await _courseCollection.Find(s => s.UserId.Equals(userId)).ToListAsync();

            if (courses.Count <= 0)
            {
                return Response<List<CourseDto>>.Success(404);
            }

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(s => s.Id.Equals(course.CategoryId)).FirstAsync();
                }
            }

            else
            {
                courses = new List<CourseEntity>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
    }
}

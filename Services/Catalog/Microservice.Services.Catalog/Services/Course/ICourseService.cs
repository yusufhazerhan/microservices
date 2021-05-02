using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.Services.Catalog.Dtos.Course;
using Microservice.Shared.Dtos;

namespace Microservice.Services.Catalog.Services.Course
{
    public interface ICourseService
    {
        Task<Response<CourseDto>> Create(CourseCreateDto request);
        Task<Response<NoContent>> Update(CourseUpdateDto request);
        Task<Response<NoContent>> Delete(string id);
        Task<Response<List<CourseDto>>> Get();
        Task<Response<CourseDto>> Get(string id);
        Task<Response<List<CourseDto>>> GetByUserId(string userId);
    }
}

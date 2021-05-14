using AutoMapper;
using Microservice.Services.Catalog.Dtos.Category;
using Microservice.Services.Catalog.Dtos.Course;
using Microservice.Services.Catalog.Dtos.Feature;
using Microservice.Services.Catalog.Models;

namespace Microservice.Services.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<CourseEntity, CourseDto>().ReverseMap();
            CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
            CreateMap<FeatureEntity, FeatureDto>().ReverseMap();

            CreateMap<CourseEntity, CourseCreateDto>().ReverseMap();
            CreateMap<CourseEntity, CourseUpdateDto>().ReverseMap();
        }
    }
}

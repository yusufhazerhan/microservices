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
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
        }
    }
}

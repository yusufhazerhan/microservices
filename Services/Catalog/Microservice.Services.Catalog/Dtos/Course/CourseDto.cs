﻿using System;
using Microservice.Services.Catalog.Dtos.Category;
using Microservice.Services.Catalog.Dtos.Feature;

namespace Microservice.Services.Catalog.Dtos.Course
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CategoryId { get; set; }
        public FeatureDto Feature { get; set; }
        public CategoryDto Category { get; set; }
    }
}
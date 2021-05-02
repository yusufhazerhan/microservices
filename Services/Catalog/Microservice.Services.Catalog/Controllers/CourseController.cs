using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Services.Catalog.Dtos.Course;
using Microservice.Services.Catalog.Services.Course;
using Microservice.Shared.BaseController;
using Microservice.Shared.Dtos;

namespace Microservice.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto request)
        {
            var response = await _courseService.Create(request);
            return ReturnResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto request)
        {
            var response = await _courseService.Update(request);
            return ReturnResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.Delete(id);
            return ReturnResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _courseService.Get();
            return ReturnResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _courseService.Get(id);
            return ReturnResponse(response);
        }

        [HttpGet]
        [Route("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var response = await _courseService.GetByUserId(userId);
            return ReturnResponse(response);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Services.Catalog.Dtos.Category;
using Microservice.Services.Catalog.Models;
using Microservice.Services.Catalog.Services.Category;
using Microservice.Shared.BaseController;
using Microservice.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto request)
        {
            var response = await _categoryService.Create(request);
            return ReturnResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto request)
        {
            var response = await _categoryService.Update(request);
            return ReturnResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _categoryService.Delete(id);
            return ReturnResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _categoryService.Get();
            return ReturnResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _categoryService.Get(id);
            return ReturnResponse(response);
        }
    }
}

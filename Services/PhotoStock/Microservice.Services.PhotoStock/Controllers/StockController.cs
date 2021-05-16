using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microservice.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Microservice.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : BaseController
    {
        private readonly IConfiguration _configuration;

        public StockController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Save(IFormFile file, CancellationToken cancellationToken)
        {
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), _configuration["UploadPath"], file.FileName);
                await using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream, cancellationToken);

                return Ok($"{_configuration["UploadPath"]}{file.FileName}");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(string url)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), _configuration["UploadPath"], url);
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            System.IO.File.Delete(path);
            return Ok();
        }
    }
}

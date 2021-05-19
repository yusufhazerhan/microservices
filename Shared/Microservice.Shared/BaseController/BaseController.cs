using System;
using System.Collections.Generic;
using System.Text;
using Microservice.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Shared.BaseController
{
    public class BaseController : ControllerBase
    {
        public IActionResult ReturnResponse<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }
    }
}

using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace Microservice.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        [JsonIgnore]
        public bool IsSuccess { get; set; }
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        public static Response<T> Success(T data, HttpStatusCode statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccess = true };
        }

        public static Response<T> Success(HttpStatusCode statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccess = true };
        }

        public static Response<T> Fail(List<string> errors, HttpStatusCode statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccess = false };
        }

        public static Response<T> Fail(string error, HttpStatusCode statusCode)
        {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccess = false };
        }
    }
}

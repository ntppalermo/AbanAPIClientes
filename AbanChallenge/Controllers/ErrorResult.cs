using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbanChallenge.Controllers
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ErrorResult : ObjectResult
    {
        public ErrorResult(int statusCode, string message) : base(new { StatusCode = statusCode, Message = message })
        {
            StatusCode = statusCode;
        }
    }
}

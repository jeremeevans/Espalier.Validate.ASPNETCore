using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Linq;
using System.Net;

namespace Espalier.Validate.ASPNETCore
{
    public class EspalierExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var espalierValidationException = context.Exception as EspalierValidationException;

            if(espalierValidationException == null)
            {
                return;
            }

            var jsonApiErrorsResponse = new
            {
                errors = espalierValidationException.ValidationErrors.Select(error => new
                {
                    param = error.PropertyName,
                    messages = error.ErrorMessages
                }).ToArray()
            };

            var jsonResponse = JsonConvert.SerializeObject(jsonApiErrorsResponse);

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new JsonResult(jsonResponse);
            context.ExceptionHandled = true;
        }
    }
}
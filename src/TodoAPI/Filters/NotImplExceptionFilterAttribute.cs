using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
//using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoAPI.Filters
{
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute 
    {
        public override void OnException(ExceptionContext context)
        {
            //context.Result = new ObjectResult("Error message from filter");
            LogException(context);
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {            
            await LogException(context);             
        }
        Task LogException(ExceptionContext context)
        {
            
            context.Result = new ObjectResult(context.Exception);
            context.Result = new JsonResult(context.Exception)
                                {
                                    StatusCode = (int)HttpStatusCode.InternalServerError
                                };
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Task.CompletedTask;
        }
    }
}
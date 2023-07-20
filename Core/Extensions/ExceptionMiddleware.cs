using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        /*
         ASP.NET Core veya diğer frameworklerdeki bir HTTP talebini işlerken kullanılan komponentlerdir. Middleware'ler, gelen talepleri alır, isteği işler ve yanıtı oluşturur.

         Middleware'ler zincir şeklinde çalışır ve her middleware, işlemi gerçekleştirirken bir sonraki middleware'e geçer. Ancak, bir middleware'de hata meydana gelirse, işlem zinciri durur ve hata, hatanın meydana geldiği middleware'in önceki middleware'lere geri iletilmesiyle işlenir.

         Yani, bir hata meydana geldiğinde, hata zinciri geriye doğru hareket eder ve hatayı ele alabilecek bir Exception Middleware bulunana kadar devam eder. Exception Middleware, hatayı yakalayabilir, işleyebilir ve uygun bir yanıt döndürebilir.

         Bu sayede, bir önceki middleware'de meydana gelen hatayı Exception Middleware'e iletir ve orada özel bir işlem yapılması sağlanır.
         */

        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors;
            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = 400,
                    Message = message,
                    Errors = errors
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}

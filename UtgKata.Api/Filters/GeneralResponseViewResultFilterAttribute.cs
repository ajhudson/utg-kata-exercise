using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using UtgKata.Api.Models;

namespace UtgKata.Api.Filters
{
    public class GeneralResponseViewResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {

            var result = context.Result as ObjectResult;
            int statusCode = result.StatusCode ?? 0;
            bool isSuccessOrRedirection = statusCode >= 200 && statusCode < 400;
            var err = result.Value as ErrorMessageViewModel;
            
            var resp = new GeneralResponseViewModel
            {
                Response = isSuccessOrRedirection ? result.Value : null,
                HasErrors = !isSuccessOrRedirection,
                ErrorDetails = err
            };

            var objectResult = new ObjectResult(resp);
            objectResult.StatusCode = statusCode;

            context.Result = objectResult;

            await next();
        }
    }
}

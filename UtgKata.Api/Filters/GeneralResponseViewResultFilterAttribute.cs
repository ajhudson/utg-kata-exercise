// <copyright file="GeneralResponseViewResultFilterAttribute.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Filters
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using UtgKata.Api.Models;

    /// <summary>
    ///   The GeneralResponseViewResultFilterAttribute.
    /// </summary>
    public class GeneralResponseViewResultFilterAttribute : ResultFilterAttribute
    {
        /// <inheritdoc />
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
                ErrorDetails = err,
            };

            var objectResult = new ObjectResult(resp);
            objectResult.StatusCode = statusCode;

            context.Result = objectResult;

            await next();
        }
    }
}

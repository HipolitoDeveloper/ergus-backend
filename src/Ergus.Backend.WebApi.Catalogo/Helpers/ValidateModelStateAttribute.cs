using Ergus.Backend.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ergus.Backend.WebApi.Catalogo.Helpers
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                if (errors.Any())
                {
                    var nonEmptyErrorIndice = errors.FindIndex(e => e == "A non-empty request body is required.");

                    if (nonEmptyErrorIndice >= 0)
                        errors[nonEmptyErrorIndice] = "O objeto deve ser preenchido";
                }

                context.Result = new ApiResult(new BadRequestApiResponse(new { Erros = errors }));
            }
        }
    }
}

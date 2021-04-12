using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CherryDough.Services.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();

        protected ActionResult ShowcaseResponse(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Message", _errors.ToArray() }
            }));
        }

        protected ActionResult ShowcaseResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return ShowcaseResponse();
        }

        protected bool IsOperationValid()
        {
            return !_errors.Any();
        }

        protected void AddError(string error)
        {
            _errors.Add(error);
        }

        protected void ClearError()
        {
            _errors.Clear();
        }
    }
}
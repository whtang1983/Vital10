using Microsoft.AspNetCore.Http;
using Vital10.Domain.SeedWork;

namespace Vital10.API.SeedWork
{
    public class BusinessRuleValidationExceptionProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            this.Title = "Business rule validation error";
            this.Status = StatusCodes.Status409Conflict;
            this.Detail = exception.Details;
            this.Type = "https://vital10.nl/business-rule-validation-error";
        }
    }
}
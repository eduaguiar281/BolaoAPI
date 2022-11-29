using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolao.Core.Validations
{
    public static class ValidationExtensions
    {
        public static Result ValidateToResult(this IValidatableObject @object)
        {
            List<ValidationResult> validationResults = new();
            if (Validator.TryValidateObject(@object, new ValidationContext(@object, null, null), validationResults))
                return Result.Success();

            StringBuilder messages = new();
            validationResults.ForEach(v => messages.AppendLine(v.ErrorMessage));
            return Result.Failure(messages.ToString());
        }
    }
}

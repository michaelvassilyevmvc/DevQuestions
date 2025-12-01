using FluentValidation.Results;
using Shared;

namespace DevQuestions.Application.Extensions;

public static class ValidationExtensions
{
    public static Error[] ToErrors(this ValidationResult validationResult) => validationResult.Errors.Select(e =>
            Error.Validation(e.ErrorCode, e.PropertyName, e.ErrorMessage))
        .ToArray();
}
using FluentValidation.Results;

namespace Shared.Extensions;

public static class ValidationExtensions
{
    public static Failure ToErrors(this ValidationResult validationResult) => validationResult.Errors.Select(e =>
            Error.Validation(e.ErrorCode, e.PropertyName, e.ErrorMessage))
        .ToArray();
}
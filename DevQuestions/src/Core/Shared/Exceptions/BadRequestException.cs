using System.Text.Json;

namespace Shared.Exceptions;

public class BadRequestException : Exception
{
    protected BadRequestException(Error[] error)
        : base(JsonSerializer.Serialize(error))
    {
    }
}
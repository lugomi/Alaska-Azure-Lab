using LabService.Models;

namespace LabService.Exceptions;

public class BadRequestException : Exception
{
    public Error Error { get; }

    public BadRequestException(string message) : base(message)
    {
        Error = new Error {Message = message};
    }
}
using LabService.Models;

namespace LabService.Exceptions;

public class NotFoundException : Exception
{
    public Error Error { get; }

    public NotFoundException(string message) : base(message)
    {
        Error = new Error { Message = message };
    }
}
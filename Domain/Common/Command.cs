using MediatR;

namespace Domain.Common;

public class Command : IRequest
{
    public Command(string topic)
    {
        Topic = topic;
    }

    public string Topic { get; }
}
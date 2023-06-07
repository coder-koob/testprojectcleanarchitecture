using MediatR;

namespace Domain.Common;

public class Command<T> : IRequest
{
    public Command(string topic, T payload)
    {
        Topic = topic;
        Payload = payload;
    }

    public string Topic { get; }
    public T Payload { get; }
}
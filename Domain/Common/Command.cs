using MediatR;

namespace Domain.Common;

public abstract class Command<T> : IRequest
{
    public Command(string topic, T payload)
    {
        Topic = topic;
        Payload = payload;
    }

    public string Topic { get; }
    public T Payload { get; }
}

public abstract class Command<T,R> : IRequest<R>
{
    public Command(string topic, T payload)
    {
        Topic = topic;
        Payload = payload;
    }

    public string Topic { get; }
    public T Payload { get; }
}
using MediatR;

namespace Domain.Common;

public abstract class Command<TPayload> : IRequest
{
    protected Command(TPayload payload)
    {
        Payload = payload;
    }

    public TPayload Payload { get; }
}

public abstract class Command<TPayload,TResponse> : IRequest<TResponse>
{
    protected Command(TPayload payload)
    {
        Payload = payload;
    }

    public TPayload Payload { get; }
}

using MediatR;

namespace Domain.Common;

public interface ICommand<TPayload>
{
    public TPayload Payload { get; }
    public Context? Context { get; set; }
}

public abstract class Command<TPayload> : IRequest, IContextAware, ICommand<TPayload>
{
    protected Command(TPayload payload)
    {
        Payload = payload;
    }

    public TPayload Payload { get; }
    public Context? Context { get; set; }
}

public abstract class Command<TPayload,TResponse> : IRequest<TResponse>, IContextAware, ICommand<TPayload>
{
    protected Command(TPayload payload)
    {
        Payload = payload;
    }

    public TPayload Payload { get; }
    public Context? Context { get; set; }
}

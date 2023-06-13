using Domain.Interfaces;
using MediatR;

namespace Domain.Common;

public abstract class BaseCommandHandler
{
    private readonly ICurrentUserService _currentUserService;
    private Context? _context;

    protected BaseCommandHandler(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public Context Context => _context ??= new Context { ClientId = _currentUserService.ClientId };
}

public abstract class CommandHandler<TRequest> : BaseCommandHandler, IRequestHandler<TRequest>
    where TRequest : IRequest, IContextAware
{
    protected CommandHandler(ICurrentUserService currentUserService)
        : base(currentUserService)
    {
    }

    public async Task Handle(TRequest request, CancellationToken cancellationToken)
    {
        request.Context = Context;
        await HandleCommand(request, cancellationToken);
    }

    protected abstract Task HandleCommand(TRequest command, CancellationToken cancellationToken);
}

public abstract class CommandHandler<TRequest, TResponse> : BaseCommandHandler, IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IContextAware
{
    protected CommandHandler(ICurrentUserService currentUserService)
        : base(currentUserService)
    {
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        request.Context = Context;
        return await HandleCommand(request, cancellationToken);
    }

    protected abstract Task<TResponse> HandleCommand(TRequest command, CancellationToken cancellationToken);
}

using MediatR;

namespace Domain.Common;

public abstract class CommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        return await HandleCommand(request, cancellationToken);
    }

    protected abstract Task<TResponse> HandleCommand(TRequest command, CancellationToken cancellationToken);
}
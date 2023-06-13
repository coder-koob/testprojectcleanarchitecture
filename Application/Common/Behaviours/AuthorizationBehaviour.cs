using System.Reflection;
using Application.Common.Security;
using Application.Common.Services;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IClientService _clientService;

    public AuthorizationBehaviour(
        ICurrentUserService currentUserService,
        IClientService identityService)
    {
        _currentUserService = currentUserService;
        _clientService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_currentUserService.ClientId == null)
            {
                throw new UnauthorizedAccessException();
            }

            // Policy-based authorization
            var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Scope));
            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (var scope in authorizeAttributesWithPolicies.Select(a => a.Scope))
                {
                    var authorized = _clientService.IsClientAuthorized(_currentUserService.ClientId, scope);

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}
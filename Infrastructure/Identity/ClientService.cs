using Application.Common.Services;
using Duende.IdentityServer.Models;

namespace Infrastructure.Identity;

public class ClientService : IClientService
{
    private readonly IEnumerable<Client> _clients;

    public ClientService(IEnumerable<Client> clients)
    {
        _clients = clients;
    }

    public bool IsClientAuthorized(string clientId, params string[] scopes)
    {
        var client = _clients.FirstOrDefault(c => c.ClientId == clientId);

        if (client == null)
        {
            return false;
        }

        foreach (var scope in scopes)
        {
            return client.AllowedScopes.Contains(scope);
        }

        return false;
    }
}

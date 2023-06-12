namespace Application.Common.Services;

public interface IClientService
{
    bool IsClientAuthorized(string clientId, params string[] scopes);
}
namespace Application.Common.Services;

public interface IClientService
{
    bool IsClientAuthorized(string clientId, string scope);
}
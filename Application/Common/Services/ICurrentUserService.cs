namespace Application.Common.Services;

public interface ICurrentUserService
{
    string? ClientId { get; }
}
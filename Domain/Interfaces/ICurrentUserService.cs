namespace Domain.Interfaces;

public interface ICurrentUserService
{
    string? ClientId { get; }
}
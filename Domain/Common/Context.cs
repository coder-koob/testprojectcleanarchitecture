namespace Domain.Common;

public class Context
{
    public Guid? CorrelationId { get; set; }

    public string? ClientId { get; set; }
}
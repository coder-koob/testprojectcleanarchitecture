namespace Domain.Common;

public interface IContextAware
{
    Context? Context { get; set; }
}
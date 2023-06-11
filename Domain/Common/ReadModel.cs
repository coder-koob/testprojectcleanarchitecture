namespace Domain.Common;

public abstract class ReadModel
{
    private readonly string _hashSet;

    protected ReadModel(string hashSet)
    {
        _hashSet = hashSet;
    }

    public abstract int Version { get; }

    public virtual string GenerateHashSetName(string? id = null)
    {
        return string.IsNullOrEmpty(id) ? $"{_hashSet}|{Version}" : $"{_hashSet}|{Version}|{id}";
    }
}
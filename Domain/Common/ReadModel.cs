namespace Domain.Common;

public abstract class ReadModel
{
    protected ReadModel(string hashSet)
    {
        HashSet = hashSet;
    }

    public string HashSet { get; set; }

    public abstract int Version { get; }

    public virtual string GenerateHashSetName() => $"{HashSet}|{Version}";
}
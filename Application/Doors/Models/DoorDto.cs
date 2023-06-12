namespace Application.Doors.Models;

public class DoorDto
{
    public DoorDto()
    {
    }

    public DoorDto(Guid officeId, Guid doorId, DateTimeOffset timestamp)
    {
        OfficeId = officeId;
        DoorId = doorId;
        Timestamp = timestamp;
    }

    public DoorDto(Guid officeId, Guid doorId, string? name, DateTimeOffset timestamp)
    {
        OfficeId = officeId;
        DoorId = doorId;
        Name = name;
        Timestamp = timestamp;
    }

    public DoorDto(Guid officeId, Guid doorId, string? name, bool isLocked, DateTimeOffset timestamp, string scope)
    {
        OfficeId = officeId;
        DoorId = doorId;
        Name = name;
        IsLocked = isLocked;
        Timestamp = timestamp;
        Scope = scope;
    }

    public Guid OfficeId { get; set; }
    public Guid DoorId { get; set; }
    public string? Name { get; set; }
    public bool IsLocked { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string? Scope { get; set; }
}
namespace Application.Doors.Models;

public class DoorHistoryDto
{
    public DoorHistoryDto(Guid officeId, Guid doorId, IList<DoorEventDto> doorHistory, DateTimeOffset timestamp)
    {
        OfficeId = officeId;
        DoorId = doorId;
        DoorHistory = doorHistory;
        Timestamp = timestamp;
    }

    public Guid OfficeId { get; set; }

    public Guid DoorId { get; set; }

    public IList<DoorEventDto> DoorHistory { get; set; } = new List<DoorEventDto>();

    public DateTimeOffset Timestamp { get; set; }
}